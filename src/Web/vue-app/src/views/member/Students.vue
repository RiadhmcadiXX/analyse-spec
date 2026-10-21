<template>
  <div class="content-grid content-grid--subpage content-grid--subpage-table">
    <div>
      <div style="display: flex; align-items: center; padding-bottom: 1em; gap: 1em; padding-bottom: 1em; padding-top: 1em; display: flex; align-items: center; justify-content: center;">
        <div style="display: block;">
          <div>
            <h1 style="text-align:center; margin-bottom: 1em;">{{ $t('studentProfile.searchForStudent') }}</h1>
            <div class="content-grid__filters">
              <div class="search-input" style="background-color: white;">
                <input v-model="searchStudent" type="search" name="searchStudent" id="searchStudent" :placeholder="$t('studentProfile.searchForStudent')" style="min-width: 20ch; width: 25vw; max-width: 400px;">
                <label for="searchStudent">
                  <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 17 18" class="icon icon--black">
                    <path fill="#000" fill-rule="evenodd" d="m12.886 11.568 3.853 3.852a1.024 1.024 0 0 1-1.445 1.446l-3.854-3.853a7.12 7.12 0 0 1-4.284 1.427A7.164 7.164 0 0 1 0 7.284 7.164 7.164 0 0 1 7.157.127c3.946 0 7.156 3.21 7.156 7.157a7.118 7.118 0 0 1-1.427 4.283Zm-5.73-9.396a5.112 5.112 0 0 0-5.11 5.112l-.002-.001a5.118 5.118 0 0 0 5.112 5.112 5.112 5.112 0 0 0 0-10.223Z" clip-rule="evenodd"></path>
                  </svg>
                </label>
              </div>
            </div>
          </div>
          <div v-if="searchStudent !== '' && filteredStudents.length > 0" style="border: 2px solid black; background: white; width: 100%; left: 0; position: absolute; z-index: 1000; box-shadow: 0px 4px 6px rgba(0,0,0,0.1);">
            <div v-for="studentName in filteredStudents" style="margin-left: 1em; margin-bottom: 0.5em; margin-top: 0.5em" :key="studentName">
              <button class="student-choice" @click="getSpecificStudent(studentName)" type="button" :id="studentName">
                {{ studentName }}
              </button>
            </div>
          </div>
        </div>
      </div>

      <div v-if="selectedStudent != null" style="font-family: system-ui; margin-bottom: 1em; border: 2px solid black; padding: 10px; border-radius: 10px; background: #7fba27;">
        <h1 style="margin-bottom: 1em; color: white; text-decoration: underline; font-size: 38px; padding-top: 0.3em; text-align: center;">{{ selectedStudent.firstAndLastName }}</h1>
        <div style="display: flex; justify-content: space-between; margin-left: 4em; margin-inline-end: 4em;">
          <div>
            <h2 style="margin-bottom: 1em;">{{ $t('studentProfile.studentIsStuding') }} : {{ selectedStudent.cegep }}</h2>
            <h2 style="margin-bottom: 1em;">DA : {{ selectedStudent.identification }}</h2>
            <h2 style="margin-bottom: 1em;">Adresse courriel : {{ selectedStudent.collEmail }}</h2>
            <h2 style="margin-bottom: 1em;">Sexe : {{ selectedStudent.gender }}</h2>
            <h2 style="margin-bottom: 1em;">{{ $t('studentProfile.studentBirthDayDate') }} : {{ formattedBirthday }}</h2>
            <h2 style="margin-bottom: 1em;">Programme : {{ selectedStudent.choice }}</h2>
          </div>
          <div>
            <h2 style="margin-bottom: 1em;">Cohorte : {{ selectedStudent.cohort }}</h2>
            <h2 style="margin-bottom: 1em;">{{$t('studentProfile.completionStatus')}} : {{ booleanToYesNo(selectedStudent.consSuccess) }}</h2>
            <h2 style="margin-bottom: 1em;">Code programme : {{ selectedStudent.progSemTarget }}</h2>
          </div>
        </div>
      </div>

      <div v-if="selectedStudent != null" style="border: 2px solid black; font-family: system-ui; ">
        <h2 style="text-align: center; margin-top:1em; margin-bottom: 1em;">Nombre de questions : {{ questionCount }}</h2>

        <div style="display: flex; justify-content: center; border-bottom: 2px solid black;">
          <div style="display: flex; align-items: center; padding-bottom: 1em; gap: 1em; padding-top: 1em; padding-right: 2em; justify-content: center;">
            <h1>{{ $t('studentProfile.searchForQuestion') }}</h1>
            <div class="content-grid__filters">
              <div class="search-input" style="margin-right: 1em;">
                <input v-model="searchQ" type="search" name="searchQuestion" id="searchQuestion" :placeholder="$t('studentProfile.searchForQuestion')" style="min-width: 12ch; width: 15vw; max-width: 300px;">
                <label for="searchQuestion">
                  <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 17 18" class="icon icon--black">
                    <path fill="#000" fill-rule="evenodd" d="m12.886 11.568 3.853 3.852a1.024 1.024 0 0 1-1.445 1.446l-3.854-3.853a7.12 7.12 0 0 1-4.284 1.427A7.164 7.164 0 0 1 0 7.284 7.164 7.164 0 0 1 7.157.127c3.946 0 7.156 3.21 7.156 7.157a7.118 7.118 0 0 1-1.427 4.283Zm-5.73-9.396a5.112 5.112 0 0 0-5.11 5.112l-.002-.001a5.118 5.118 0 0 0 5.112 5.112 5.112 5.112 0 0 0 0-10.223Z" clip-rule="evenodd"></path>
                  </svg>
                </label>
              </div>
            </div>
          </div>

          <div style="display: flex; align-items: center; padding-bottom: 1em; gap: 1em; padding-top: 1em; justify-content: center;">
            <h1>{{ $t('studentProfile.searchForAnswer') }}</h1>
            <div class="content-grid__filters">
              <div class="search-input">
                <input v-model="searchA" type="search" name="searchAnswer" id="searchAnswer" :placeholder="$t('studentProfile.searchForAnswer')" style="min-width: 12ch; width: 15vw; max-width: 300px;">
                <label for="searchAnswer">
                  <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 17 18" class="icon icon--black">
                    <path fill="#000" fill-rule="evenodd" d="m12.886 11.568 3.853 3.852a1.024 1.024 0 0 1-1.445 1.446l-3.854-3.853a7.12 7.12 0 0 1-4.284 1.427A7.164 7.164 0 0 1 0 7.284 7.164 7.164 0 0 1 7.157.127c3.946 0 7.156 3.21 7.156 7.157a7.118 7.118 0 0 1-1.427 4.283Zm-5.73-9.396a5.112 5.112 0 0 0-5.11 5.112l-.002-.001a5.118 5.118 0 0 0 5.112 5.112 5.112 5.112 0 0 0 0-10.223Z" clip-rule="evenodd"></path>
                  </svg>
                </label>
              </div>
            </div>
          </div>
        </div>

        <div v-if="searchQ === '' && searchA === ''">

          <div>
            <div v-for="(question, index) in paginatedParentQuestions" :key="question.id" style="border-bottom: 1px solid">
              <p @click="toggleChildren(question.id)" :style="{ padding: '1em', backgroundColor: '#dfdfdf', cursor: 'pointer' }">
                <strong>Question {{ (actualPageNumber - 1) * maxQuestionPerPage + index + 1 }}: {{ question.title }}</strong>
              </p>

              <div v-for="(item) in filteredOptions(question.id)" :key="item.idOption.id"
                   :style="{ padding: '1em', backgroundColor: '#ffffff' }">
                {{ item.idOption.title }}
              </div>

              <div v-if="question.children.length > 0 && showChildren[question.id]">
                <div v-for="(child, childIndex) in question.children" :key="child.id">
                  <p :style="{ padding: '1em', backgroundColor: '#dfdfdf' }">
                    <strong>Sous question {{ childIndex + 1 }}: {{ child.title }}</strong>
                  </p>

                  <div v-for="(item) in filteredOptions(child.id)" :key="item.idOption.id"
                       :style="{ padding: '1em', backgroundColor: '#ffffff' }">
                    {{ item.idOption.title }}
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div style="margin: 2em;">
            <div style="display: flex; justify-content: center; align-items: center;">
              <div style="display: flex; flex-direction: column; align-items: center;">
                <button style="border: none; background: none; cursor: pointer; padding: 0;" @click="PreviousPage()">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" width="24" height="24">
                    <path d="M9.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.2 288 416 288c17.7 0 32-14.3 32-32s-14.3-32-32-32l-306.7 0L214.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z" />
                  </svg>
                  <p style="margin-top: 5px;">{{ $t('studentProfile.studentLastPage') }}</p>
                </button>
              </div>
              <p style="margin-left: 5em; margin-right: 5em; display: flex; align-items: center;">{{ actualPageNumber }} / {{ nbQuestionPage }}</p>
              <div style="display: flex; flex-direction: column; align-items: center;">
                <button style="border: none; background: none; cursor: pointer; padding: 0;" @click="NextPage()">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" width="24" height="24">
                    <path d="M438.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-160-160c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L338.8 224 32 224c-17.7 0-32 14.3-32 32s14.3 32 32 32l306.7 0L233.4 393.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l160-160z" />
                  </svg>
                  <p style="margin-top: 5px;">{{ $t('studentProfile.studentNextPage') }}</p>
                </button>
              </div>
            </div>
          </div>

        </div>

        <div v-if="searchQ !== '' || searchA !== ''">

          <div v-for="(item, index) in filteredQuestions" :key="index">
            <p :style="{ padding: '1em', backgroundColor: '#dfdfdf' }">
              <strong>Question. {{ item.question }}</strong>
            </p>
            <p :style="{ padding: '1em', backgroundColor: '#ffffff' }">
              <strong>{{ item.answer }}</strong>
            </p>
          </div>

        </div>
      </div>


      <div v-if="selectedStudent == null" class="content-grid content-grid--subpage content-grid--subpage-table">
        <!--            <div class="content-grid__header">-->
        <!--              <h1 class="back-link-title">Liste des étudiants</h1>-->
        <!--              -->
        <!--            </div>-->

        <Card>
          <DataTable :headers="etudiantHeader" :search-value="searchValue" :is-loading="enChargement" :items="tableStudents" @rowClick="handleRowClick"/>
        </Card>
      </div>
    </div>
  </div>
</template>


<script lang="ts" setup>
import { onMounted, ref, computed, watch } from "vue"
import { useStudentService, useAnswerService, useOptionService, useQuestionService } from "@/inversify.config";
import { Student, Answer, Option, Question } from "@/types/entities";
import { Guid } from "@/types";
import Card from "@/components/layouts/items/Card.vue";
import SearchInput from "@/components/layouts/items/SearchInput.vue";
import DataTable from "@/components/layouts/items/DataTable.vue";

const searchQ = ref("");
const searchA = ref("");
const searchStudent = ref("");
const studentService = useStudentService();
const answerService = useAnswerService();
const optionService = useOptionService();
const questionService = useQuestionService();
const allStudents = ref<Student[]>([]);
const allAnswers = ref<Answer[]>([]);
const allOptions = ref<Option[]>([]);
const allQuestions = ref<Question[]>([]);
const selectedStudent = ref<Student | null>(null);
let studentsDatas: Student[] = [];
let answersDatas: Answer[] = [];
let optionsDatas: Option[] = [];
let questionsDatas: Question[] = [];
const questionsAndAnswers = ref<{ question: string; answer: string }[]>([]);
const optionsForComponent = ref<any[]>([]);
const questionsForComponent = ref<any[]>([]);
const showChildren = ref<{ [key: string]: boolean }>({});
const nbQuestionPage = ref<number>(0);
const actualPageNumber = ref<number>(1);
const maxQuestionPerPage = 10;

onMounted(async () => {
  await GetAllStudents();
});

async function GetAllStudents() {
  let students = await studentService.getAllStudents();
  if (students && students.length > 0) {
    studentsDatas = students;
    allStudents.value = students;
  }
}

async function getAnswersFromAStudent(studentId?: Guid) {
  let answers = await answerService.getAnswersByStudentId(studentId);

  if (answers && answers.length > 0) {
    answersDatas = answers;
    allAnswers.value = answers;
  } else {
    return;
  }

  let optionsIds: Array<Guid> = [];
  let i = 0;

  while (i < answersDatas.length) {
    let optionId = answersDatas[i]?.idOption?.id;
    if (optionId) {
      optionsIds.push(optionId);
    }
    i++;
  }

  questionsDatas = [];
  allOptions.value = [];
  i = 0;
  while (i < optionsIds.length) {
    await getOptionById(optionsIds[i]);
    i++;
  }
}

async function getOptionById(optionId: Guid) {

  let option = await optionService.getOptionById(optionId);
  if (option) {
    questionsDatas.push(option.idQuestion);
    allOptions.value[allOptions.value.length] = option;
    await getQuestionById(option.idQuestion?.id);
  }

  optionsForComponent.value = transformAnswersData(allOptions.value)
  console.log("optionsForComponent")
  console.log(optionsForComponent.value)
}

async function getQuestionById(questionId: Guid) {
  let question = await questionService.getQuestionById(questionId);
  if (question) {
    if (!allQuestions.value.some(q => q.id === question.id)) {
      allQuestions.value[allQuestions.value.length] = question;
    }
  }

  questionsAndAnswers.value = [];

  let i = 0;
  while (i < allQuestions.value.length) {
    questionsAndAnswers.value.push({
      question: allQuestions.value[i].title,
      answer: allOptions.value[i].title
    });
    i++;
  }

  questionsForComponent.value = buildQuestionTree(allQuestions.value)
  nbQuestionPage.value = Math.ceil(parentQuestions.value.length / maxQuestionPerPage);
}

function getSpecificStudent(studentName: string) {
  allAnswers.value = [];
  allOptions.value = [];
  allQuestions.value = [];
  selectedStudent.value = null;
  answersDatas = [];
  optionsDatas = [];
  questionsDatas = [];
  questionsAndAnswers.value = [];

  let i = 0;
  let studentFound = false;
  while (i < studentsDatas.length && !studentFound) {
    if (studentsDatas[i].firstAndLastName === studentName) {
      getAnswersFromAStudent(studentsDatas[i].id);
      selectedStudent.value = studentsDatas[i];
      searchStudent.value = "";
      console.log(selectedStudent)
      studentFound = true;
    }
    i++;
  }
}

function booleanToYesNo(value: boolean): string {
  return value ? 'Oui' : 'Non';
}

const formattedBirthday = computed(() => {
  return selectedStudent.value ? selectedStudent.value.birthday.split('T')[0] : '';
});

const filteredQuestions = computed(() =>
    questionsAndAnswers.value.filter(q =>
        q.question.toLowerCase().includes(searchQ.value.toLowerCase()) &&
        q.answer.toLowerCase().includes(searchA.value.toLowerCase())
    )
);

const filteredStudents = computed(() =>
    allStudents.value
        .filter(student =>
            student.firstAndLastName?.toLowerCase().includes(searchStudent.value?.toLowerCase() || "")
        )
        .map(student => student.firstAndLastName || "")
);

const questionCount = computed(() => {
  return questionsAndAnswers.value.length;
});

function buildQuestionTree(questions) {
  const questionMap = new Map();

  questions.forEach(q => {
    questionMap.set(q.id, { id: q.id, title: q.title, children: [] });
  });

  let rootQuestions = [];

  questions.forEach(q => {
    if (q.parent) {
      const parent = questionMap.get(q.parent.id);
      if (parent) {
        parent.children.push(questionMap.get(q.id));
      }
    } else {
      rootQuestions.push(questionMap.get(q.id));
    }
  });

  return rootQuestions;
}

function transformAnswersData(rawData) {
  return rawData.map(item => ({
    idOption: {
      id: item.id,
      title: item.title,
      idQuestion: {
        id: item.idQuestion.id
      }
    }
  }));
}

const parentQuestions = computed(() => {
  const parents = questionsForComponent.value.filter(question => !question.parentId);
  return parents;
});

const paginatedParentQuestions = computed(() => {
  const start = (actualPageNumber.value - 1) * maxQuestionPerPage;
  const end = start + maxQuestionPerPage;
  return parentQuestions.value.slice(start, end);
});

function filteredOptions(questionId) {
  const filtered = optionsForComponent.value.filter(option => option.idOption.idQuestion.id === questionId);
  return filtered;
}

function toggleChildren(questionId) {
  showChildren.value[questionId] = !showChildren.value[questionId];
}

function NextPage() {
  if (actualPageNumber.value < nbQuestionPage.value) {
    actualPageNumber.value++;
    console.log("Next page");
  }
}

function PreviousPage() {
  if (actualPageNumber.value > 1) {
    actualPageNumber.value--;
    console.log("Previous page");
  }
}



const etudiantHeader = [
  { text: "Numéro Da", value: "cp", sortable: true, width: 90 },
  { text: "Nom, prénom", value: "firstAndLastname", sortable: true, width: 150 },
  { text: "Email", value: "admEmail", sortable: true, width: 200 },
  { text: "Sexe", value: "biological_sex", sortable: true, width: 100 },
  { text: "Population", value: "population", sortable: true, width: 120 },
  { text: "Programme", value: "prog", sortable: true, width: 130 },
];

const tableStudents = computed(() => {
  return allStudents.value
      .filter(student => {
        const query = searchStudent.value.toLowerCase();
        return (
            student.firstAndLastName?.toLowerCase().includes(query) ||
            student.cp?.toString().includes(query) ||
            student.admEmail?.toLowerCase().includes(query)
        );
      })
      .map((x: any) => ({
        id: x.id,
        cp: x.cp,
        firstAndLastname: x.firstAndLastName,
        admEmail: x.admEmail,
        birthday: x.birthday,
        biological_sex: x.gender ?? '',
        legal_status: x.status,
        population: x.population,
        prog: x.progDec
      }));
});

function handleRowClick(student: any) {
  const studentName = student.firstAndLastname // ou un autre champ si besoin
  getSpecificStudent(studentName)
}



</script>


<style scoped>
.search-section {
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 1em 0;
}

.search-container {
  display: block;
  text-align: center;
}

.title {
  font-size: 24px;
  margin-bottom: 1em;
}

.search-input-container {
  position: relative;
  display: inline-block;
}

.search-input {
  min-width: 20ch;
  width: 25vw;
  max-width: 400px;
  padding: 0.5em;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.search-icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
  cursor: pointer;
}

.search-results {
  border: 2px solid black;
  background: white;
  width: 100%;
  left: 0;
  position: absolute;
  z-index: 1000;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
}

.search-result-item {
  margin: 0.5em 1em;
}

.student-choice {
  border-bottom: 1px solid black;
  background: none;
  border: none;
  cursor: pointer;
  padding: 0.5em;
  width: 100%;
  text-align: left;
  transition: background-color 0.3s, color 0.3s;
}

.student-choice:hover {
  background-color: #f0f0f0;
  color: #333;
}

.student-details {
  font-family: system-ui;
  margin-bottom: 1em;
  border: 2px solid black;
  padding: 10px;
  border-radius: 10px;
  background: #7fba27;
}

.student-name {
  margin-bottom: 1em;
  color: white;
  text-decoration: underline;
  font-size: 38px;
  padding-top: 0.3em;
  text-align: center;
}

.student-info {
  display: flex;
  justify-content: space-between;
  margin: 0 4em;
}

.questions-section {
  border: 2px solid black;
  font-family: system-ui;
  padding: 1em;
  border-radius: 10px;
  background: #f9f9f9;
}

.text-center {
  text-align: center;
  margin-bottom: 1em;
}

.search-input-group {
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 1em 0;
}

.search-input-container {
  position: relative;
  display: inline-block;
}

.search-input {
  min-width: 12ch;
  width: 15vw;
  max-width: 300px;
  padding: 0.5em;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.search-icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
  cursor: pointer;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity 0.5s;
}

.fade-enter, .fade-leave-to /* .fade-leave-active in <2.1.8 */ {
  opacity: 0;
}
</style>




