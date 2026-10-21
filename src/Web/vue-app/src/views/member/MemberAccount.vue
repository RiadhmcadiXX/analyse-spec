<template>
  <div class="content-grid">
    <div class="content-grid__header">
      <h1>{{ t(`routes.account.name`) }}</h1>
    </div>

    <div class="content-grid content-grid--double">
      <div class="content-grid__header">
        <h2>{{ t('pages.account.loginInfos') }}</h2>
      </div>
    </div>
  </div>

  <!-- Modal affiché uniquement si l'utilisateur doit changer son mot de passe -->
  <div v-if="showModal" class="modal-overlay">
    <div class="modal">
      <Card :title="t('routes.resetPassword.name')" class="form" :is-authentication="true">
        <Loader v-if="preventMultipleSubmit" />
        <FormInput :ref="addFormInputRef"
                   v-model="resetPasswordRequest.password"
                   :label="t('global.password')"
                   :rules="[required]"
                   name="password"
                   type="password"
                   @validated="handleValidation"/>
        <FormInput :ref="addFormInputRef"
                   v-model="resetPasswordRequest.passwordConfirmation"
                   :label="t('global.passwordConfirmation')"
                   :rules="[required]"
                   name="passwordConfirmation"
                   type="password"
                   @validated="handleValidation"/>
        <button class="btn btn--full btn--purple btn--big"
                @click="sendResetPasswordRequest"
                :disabled="preventMultipleSubmit">
          {{ t('global.submit') }}
        </button>
      </Card>
    </div>
  </div>
</template>
<script lang="ts" setup>
import { ref, onMounted } from "vue";
import { useI18n } from "vue3-i18n";
import {useAuthenticationService, useMemberService} from "@/inversify.config";
import { notifyError, notifySuccess } from "@/notify";
import { IResetPasswordRequest } from "@/types/requests";
import {Guid, Member, User} from "@/types";
import FormInput from "@/components/forms/FormInput.vue";
import Card from "@/components/layouts/items/Card.vue";
import Loader from "@/components/layouts/items/Loader.vue";
import { required } from "@/validation/rules";
import { Status } from "@/validation";
import { useUserStore } from "@/stores/userStore";
import {UserService} from "@/services";
import {useUserService} from "@/inversify.config";
import {ApiService} from "@/services";
import Cookies from "universal-cookie";



// Importer le store utilisateur
const { user } = useUserStore();
const { t } = useI18n();
const authenticationService = useAuthenticationService();
const showModal = ref(false);
const preventMultipleSubmit = ref(false);
const userService = useUserService();
const formInputs = ref<(typeof FormInput)[]>([]);
const inputValidationStatuses: Record<string, boolean> = {};
const service =  ApiService;



// eslint-disable-next-line no-undef
const props = defineProps<{
  userId: Guid;
  token: string;
}>();


const actualToken = new Cookies().get("accessToken");


console.log("This is the actual token :", actualToken);


console.log("This is the actual user id",user.id)

const resetPasswordRequest = ref<IResetPasswordRequest>({
  userId: user.id,
  token: actualToken,
  password: "",
  passwordConfirmation: "",
});

// Vérifier si l'utilisateur doit changer son mot de passe
onMounted(() => {
  if (user?.isFirstConnection) {
    console.log("Is first connection?:" + JSON.stringify(user?.isFirstConnection));
    showModal.value = true;
  }
});

function addFormInputRef(ref: typeof FormInput) {
  if (!formInputs.value.includes(ref)) formInputs.value.push(ref);
}

async function handleValidation(name: string, validationStatus: Status) {
  inputValidationStatuses[name] = validationStatus.valid;
}

async function sendResetPasswordRequest() {
  if (preventMultipleSubmit.value) return;
  preventMultipleSubmit.value = true;

  formInputs.value.forEach((x) => x.validateInput());
  if (Object.values(inputValidationStatuses).some((x) => x === false)) {
    notifyError(t("validation.errorsInForm"));
    preventMultipleSubmit.value = false;
    return;
  }

  let resetPasswordResponse = await authenticationService.resetPassword(resetPasswordRequest.value);
  useUserStore().setUser(user);
  
  if (resetPasswordResponse.succeeded) {
    notifySuccess(t("pages.resetPassword.validation.success"));

    setTimeout(() => {
      showModal.value = false;
      user.isFirstConnection = false;
    }, 1500);
  } else {
    let errorMessages = resetPasswordResponse.getErrorMessages("pages.resetPassword.validation");
    notifyError(errorMessages.length ? errorMessages[0] : t("pages.resetPassword.validation.errorOccured"));
  }

  preventMultipleSubmit.value = false;
}
</script>


<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  background: white;
  padding: 20px;
  border-radius: 5px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  max-width: 400px;
  text-align: center;
}

button {
  margin-top: 10px;
  padding: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}
</style>
