<template>
  <form class="form" novalidate @submit.prevent="handleSubmit">
    <FormRow :withThreeColumns="true">
      <FormInput :ref="addFormInputRef"
                 v-model="member.firstName"
                 :label="t('global.firstName')"
                 :rules="[required]"
                 name="firstName"
                 type="text"
                 @validated="handleValidation"/>
      <FormInput :ref="addFormInputRef"
                 v-model="member.lastName"
                 :label="t('global.lastName')"
                 :rules="[required]"
                 name="lastName"
                 type="text"
                 @validated="handleValidation"/>
      <FormInput :ref="addFormInputRef"
                 v-model="member.email"
                 :label="t('global.email')"
                 :rules="[required]"
                 name="email"
                 type="text"
                 @validated="handleValidation"/>
    </FormRow>
    <FormRow :withThreeColumns="true">
      <div class="form__2fr-1fr-fields">
        <FormInput :ref="addFormInputRef"
                   v-model="member.phoneNumber"
                   :label="t('global.phoneNumber')"
                   :placeholder="t('global.phoneNumberFormat')"
                   :rules="[required, mustMatchPhoneNumberFormat]"
                   name="phoneNumber"
                   type="tel"
                   @validated="handleValidation"/>
        <FormInput :ref="addFormInputRef"
                   v-model="member.phoneExtension"
                   :label="t('global.phoneExtension')"
                   name="phoneExtension"
                   type="number"
                   @validated="handleValidation"/>
      </div>
      <FormInput :ref="addFormInputRef"
                 v-model="member.apartment"
                 :label="t('global.apartment')"
                 name="apartment"
                 type="number"
                 @validated="handleValidation"/>
      <FormInput :ref="addFormInputRef"
                 v-model="member.street"
                 :label="t('global.street')"
                 :rules="[required]"
                 name="street"
                 type="text"
                 @validated="handleValidation"/>
    </FormRow>
    <FormRow :withThreeColumns="true">
      <FormInput :ref="addFormInputRef"
                 v-model="member.city"
                 :label="t('global.city')"
                 :rules="[required]"
                 name="city"
                 type="text"
                 @validated="handleValidation"/>
      <FormInput :ref="addFormInputRef"
                 v-model="member.zipCode"
                 :label="t('global.zipCode')"
                 :rules="[required, mustMatchZipCodeFormat]"
                 name="zipCode"
                 type="zip"
                 @validated="handleValidation"/>
      <FormMultiSelect
          name="programmes"
          label="Programmes d'études"
          v-model="selectedProgrammes"
          :options="programmeOptions"
          :required="true"
      />



    </FormRow>
    <button class="form__submit btn btn--full">{{ t('global.save') }}</button>
  </form>
</template>

<script lang="ts" setup>
import {useI18n} from "vue3-i18n"
import {notifyError} from "@/notify"
import {Status} from "@/validation"
import {Member} from "@/types/entities"
import {ref, toRef, watchEffect} from "vue"
import {
  mustMatchPhoneNumberFormat,
  mustMatchZipCodeFormat,
  required
} from "@/validation/rules";
import FormRow from "@/components/forms/FormRow.vue"
import {programmeOptions} from "@/types/enums/Programme-options";
import FormInput from "@/components/forms/FormInput.vue"
import FormMultiSelect from "@/components/forms/FormMultiSelect.vue";
import {FormOptionProgramme} from "@/types/formOptionProgramme";

// eslint-disable-next-line no-undef
const props = defineProps<{
  member?: Member
  selectedProgrammes?: FormOptionProgramme[]
}>()

// eslint-disable-next-line no-undef
const emit = defineEmits<{
  (event: "formSubmit", member: Member): void
}>()

const {t} = useI18n()

const member = toRef(props.member ?? {})
watchEffect(() => {
  if (props.member) {
    member.value = props.member
  }
})
const formInputs = ref<any[]>([])
const inputValidationStatuses: any = {}
const selectedProgrammes = toRef(props.selectedProgrammes ?? [])
watchEffect(() => {
  if (props.selectedProgrammes?.length) {
    selectedProgrammes.value = [...props.selectedProgrammes]
  }
})
console.log(JSON.stringify(selectedProgrammes.value));
console.log(JSON.stringify(member.value.programmes, null, 2))

function addFormInputRef(ref: typeof FormInput) {
  if (!formInputs.value.includes(ref) && ref)
    formInputs.value.push(ref)
}

async function handleValidation(name: string, validationStatus: Status) {
  inputValidationStatuses[name] = validationStatus.valid
}   

async function handleSubmit() {
  formInputs.value.forEach((x: typeof FormInput) => x.validateInput())
  if (Object.values(inputValidationStatuses).some(x => x === false)) {
    notifyError(t('validation.errorsInForm'))
    return
  }
  
  
    member.value.programmes = selectedProgrammes.value
    console.log("valeur du member programme avant envoie" + JSON.stringify(member.value.programmes))
    console.log("ejfbjwheafbjwhjbfjhws---->>>>>>>>>" + member.value.firstName)
  emit("formSubmit", {
    ...member.value,
    programmes: [...selectedProgrammes.value]
  })
}
</script>