<template>
    <div class="scrollable-content" style="min-height: 60vh">
        <div class="content-grid content-grid--subpage content-grid--subpage-table">

            <div style="display: flex; justify-content: center;">
                <div style="display: flex; align-items: center; padding-bottom: 1em; gap: 1em; padding-bottom: 1em; padding-top: 1em; display: flex; align-items: center; justify-content: center; margin-right: 1em;">
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
                        <div v-if="searchStudent !== '' && filteredStudents.length > 0" class="scrollable-container">
                            <div v-for="studentName in filteredStudents" style="margin-left: 1em; margin-bottom: 0.5em; margin-top: 0.5em" :key="studentName" >
                                <button class="student-choice" @click="getSpecificStudent(studentName)" type="button" :id="studentName">
                                    {{ studentName }}
                                </button>
                            </div>
                        </div>

                    </div>
                </div>

                <div v-if="selectedStudent != null" style="font-family: system-ui; margin-bottom: 0.5em; border: 2px solid black; padding: 3px; border-radius: 3px; background: #7fba27;">
                    <h1 style="margin-bottom: 0.5em; color: white; text-decoration: underline; font-size: 26px; padding-top: 0.3em; text-align: center;">{{ selectedStudent.firstAndLastName }}</h1>
                    <div style="display: flex; justify-content: space-between; margin-left: 1em; margin-inline-end: 1em;">
                        <div>
                            <h2 style="margin-bottom: 0.5em;">{{ $t('studentProfile.studentIsStuding') }} : {{ selectedStudent.cegep }}</h2>
                            <h2 style="margin-bottom: 0.5em;">DA : {{ selectedStudent.identification }}</h2>
                            <h2 style="margin-bottom: 0.5em;">Adresse courriel : {{ selectedStudent.collEmail }}</h2>
                            <h2 style="margin-bottom: 0.5em;">Sexe : {{ selectedStudent.gender }}</h2>
                            <h2 style="margin-bottom: 0.5em;">{{ $t('studentProfile.studentBirthDayDate') }} : {{ formattedBirthday }}</h2>
                            <h2 style="margin-bottom: 0.5em;">Programme : {{ selectedStudent.choice }}</h2>
                        </div>
                        <div>
                            <h2 style="margin-bottom: 0.5em;">Cohorte : {{ selectedStudent.cohort }}</h2>
                            <h2 style="margin-bottom: 0.5em;">{{$t('studentProfile.completionStatus')}} : {{ booleanToYesNo(selectedStudent.consSuccess) }}</h2>
                            <h2 style="margin-bottom: 0.5em;">Code programme : {{ selectedStudent.progSemTarget }}</h2>
                            <h2 v-if="selectedStudent.consentHelp" style="margin-bottom: 0.5em;">Veux de l'aide : ✅</h2>
                            <h2 v-else style="margin-bottom: 0.5em;">Veux de l'aide : ❌</h2>
                        </div>
                    </div>
                </div>
            </div>

            <div v-if="selectedStudent != null" style="border: 2px solid black; font-family: system-ui; ">
                <h2 style="text-align: center; margin-top:1em; margin-bottom: 1em;">Nombre de questions : {{ nbQuestion }}</h2>

                <div style="display: flex; justify-content: center; border-bottom: solid 2px black;">
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
                </div>

                <div v-for="question in questionsToShow" :key="question.id" id="searchDiv">
                    <div v-if="undefined == question.parentId">
                        <div class="green_hover_bg" @click="getDirectChildren(question.id)" :style="{ padding: '1em', backgroundColor: '#F0FAFE', display: 'flex'}">
                            <div v-if="question.children.length > 0 && !question.isOpen">
                                <p>&#11165;</p>
                            </div>
                            <div v-if="question.children.length > 0 && question.isOpen">
                                <p>&#11167;</p>
                            </div>
                            <strong>{{ question.title }}</strong>
                        </div>
                        <div v-if="!question.isOpen">
                            <div v-for="(answer, index) in question.answers" :key="index">
                                <div :style="{ padding: '1em', backgroundColor: '#ffffff'}">
                                    &nbsp;&nbsp;&nbsp;&nbsp;{{ answer }}
                                </div>
                            </div>
                        </div>
                        <div v-else>
                            <div v-for="(answer, index) in question.answers" :key="index">
                                <div :style="{ padding: '1em', backgroundColor: '#ffffff'}">
                                    &nbsp;&nbsp;&nbsp;&nbsp;{{ answer }}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div v-else>
                        <div class="green_hover_bg" @click="getDirectChildren(question.id)" :style="{ padding: '1em', backgroundColor: '#F0FAFE', display: 'flex'}">
                            <div v-if="question.children.length > 0 && !question.isOpen">
                                <p>&#11165;</p>
                            </div>
                            <div v-if="question.children.length > 0 && question.isOpen">
                                <p>&#11167;</p>
                            </div>
                            <strong>&nbsp;&nbsp;&nbsp;&nbsp;{{ question.title }}</strong>
                        </div>
                        <div v-if="!question.isOpen">
                            <div v-for="(answer, index) in question.answers" :key="index">
                                <div :style="{ padding: '1em', backgroundColor: '#ffffff'}">
                                    &nbsp;&nbsp;&nbsp;&nbsp;{{ answer }}
                                </div>
                            </div>
                        </div>
                        <div v-else>
                            <div v-for="(answer, index) in question.answers" :key="index">
                                <div :style="{ padding: '1em', backgroundColor: '#ffffff'}">
                                    &nbsp;&nbsp;&nbsp;&nbsp;{{ answer }}
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
        </div>
    </div>
</template>


<script lang="ts" setup>
    import { onMounted, ref, computed, watch, reactive, toRaw } from "vue"
    import { useStudentService, useAnswerService, useOptionService, useQuestionService } from "@/inversify.config";
    import { Student, Answer, Option, Question } from "@/types/entities";
    import { Guid } from "@/types";

    const maxQuestionPerPage = 10;

    const searchQ = ref("");
    const searchStudent = ref("");
    const studentService = useStudentService();
    const answerService = useAnswerService();
    const optionService = useOptionService();
    const questionService = useQuestionService();
    const allStudents = ref<Student[]>([]);
    const allOptions = ref<Option[]>([]);
    const selectedStudent = ref<Student | null>(null);
    const questionsTree = ref<any[]>([]);
    const nbQuestionPage = ref<number>(0);
    const actualPageNumber = ref<number>(1);
    const questionsToShow = ref<(Question & { isOpen: boolean } & { answers: string[] })[]>([]);
    const allQuestionsFromOptions = ref<Question[]>([]);
    const nbQuestion = ref<number>(0);

    let answersDatas: Answer[] = [];
    let questionFromOptions: any[] = [];

    onMounted(async () => {
        await GetAllStudents();
    });

    async function GetAllStudents() {
        let students = await studentService.getAllStudents();
        if (students && students.length > 0)
            allStudents.value = students;
    }

    async function getSpecificStudent(studentName: string) {
        setVariableToDefault();

        const student = allStudents.value.find(student => student.firstAndLastName === studentName);
        if (student) {
            await getAnswersFromAStudent(student.id);
            selectedStudent.value = student;
            searchStudent.value = "";
        }

        await getAllMissingParentsQuestions(allQuestionsFromOptions.value);
        questionsTree.value = buildQuestionTree(allQuestionsFromOptions.value);
        updateAllQuestionAnswers(questionsTree.value);
        nbQuestionPage.value = Math.ceil(parentQuestions.value.length / maxQuestionPerPage);
        insertTenRootQuestionInQuestionsToShow(actualPageNumber.value);
    }

    async function getAnswersFromAStudent(studentId?: Guid) {
        let answers = await answerService.getAnswersByStudentId(studentId);
        if (answers && answers.length > 0)
            answersDatas = answers;
        else
            return;

        let optionsIds: Array<Guid> = [];
        for (let i = 0; i < answersDatas.length; i++) {
            let optionId = answersDatas[i]?.idOption?.id;
            optionsIds.push(optionId);
        }

        allOptions.value = [];
        await Promise.allSettled(optionsIds.map(id => getOptionById(id)));
    }

    async function getOptionById(optionId: Guid) {
        let option = await optionService.getOptionById(optionId);
        if (option) {
            allOptions.value[allOptions.value.length] = option;
            await getQuestionById(option.idQuestion?.id);
        }
    }

    async function getQuestionById(questionId: Guid) {
        let question = await questionService.getQuestionById(questionId);
        if (question)
            allQuestionsFromOptions.value.push(question);
    }

    function buildQuestionTree(questions: Question[]) {
        questions.sort((a, b) => a.title.localeCompare(b.title));

        const questionMap = questions.map(q => ({
            id: q.id,
            title: q.title,
            children: [],
            parentId: q.parent ? q.parent.id : null
        }));

        const rootQuestions: any[] = [];
        nbQuestion.value = 0;

        questionMap.forEach(q => {
            if (!q.parentId) {
                rootQuestions.push(q);
                insertStringAnswerIntoEachQuestion(rootQuestions, q.id);
                nbQuestion.value++;
            }
        });

        questionMap.forEach(q => {
            if (q.parentId) {
                const parentNodes = findNodesInTree(rootQuestions, q.parentId);
                if (parentNodes.length === 0)
                    rootQuestions.push(q);
                else
                    parentNodes.forEach(parent => {
                        parent.children.push(q);
                        insertStringAnswerIntoEachQuestion(rootQuestions, q.id);
                        nbQuestion.value++;
                    });
            }
        });

        sortQuestionsByTitle(rootQuestions);

        const clearTree = deduplicateNodesByTitle(rootQuestions);
        return clearTree;
    }

    async function getAllMissingParentsQuestions(questions: Question[]) {
        for (const q of questions) {
            if (q.parent != null) {
                if (!questions.some((item) => item.id === q.parent!.id)) {
                    const missingQuestion = await questionService.getQuestionById(q.parent!.id);
                    if (missingQuestion) {
                        allQuestionsFromOptions.value.push(missingQuestion);
                        nbQuestion.value
                    }
                }
            }
        }
    }

    function findNodesInTree(nodes: any[], targetId: Guid): any[] {
        let results: any[] = [];
        nodes.forEach(node => {
            if (node.id === targetId)
                results.push(node);
            if (node.children && node.children.length > 0)
                results = results.concat(findNodesInTree(node.children, targetId));
        });
        return results;
    }

    function deduplicateNodesByTitle(nodes: any[]): any[] {
        const seenTitles = new Set<string>();
        const uniqueNodes: any[] = [];
        nodes.forEach(node => {
            if (!seenTitles.has(node.title)) {
                seenTitles.add(node.title);
                if (node.children && node.children.length > 0) {
                    node.children = deduplicateNodesByTitle(node.children);
                    nbQuestion.value--;
                }
                uniqueNodes.push(node);
            }
        });
        return uniqueNodes;
    }

    function updateAllQuestionAnswers(nodes: any[]): void {
        nodes.forEach((node) => {
            const optionsForNode = allOptions.value.filter(option => option.idQuestion?.id === node.id);

            const uniqueOptions = optionsForNode.filter((option, index, self) =>
                index === self.findIndex(o => o.id === option.id)
            );

            node.answers = uniqueOptions.map(option => option.title);

            if (node.children && node.children.length > 0)
                updateAllQuestionAnswers(node.children);
        });
    }

    function sortQuestionsByTitle(questions: any[]): any[] {
        questions.sort((a, b) => a.title.localeCompare(b.title));
        questions.forEach(q => {
            if (q.children && q.children.length > 0)
                sortQuestionsByTitle(q.children);
        });
        return questions;
    }

    async function insertStringAnswerIntoEachQuestion(questions: any[], questionId) {
        questions.forEach(q => {
            if (q.id == questionId)
                if (allOptions.value != [])
                    allOptions.value.forEach(option => {
                        if (option.idQuestion?.id === questionId) {
                            if (q.answers == null)
                                q.answers = [];
                            q.answers.push(option.title);
                        }
                    });
        });
    }

    function setVariableToDefault() {
        allOptions.value.length = 0;
        selectedStudent.value = null;
        allQuestionsFromOptions.value.length = 0;
        answersDatas.length = 0;
        questionsTree.value.length = 0;
    }

    function booleanToYesNo(value: boolean): string {
        return value ? 'Oui' : 'Non';
    }

    const formattedBirthday = computed(() => {
        return selectedStudent.value ? selectedStudent.value.birthday.split('T')[0] : '';
    });


    const filteredStudents = computed(() =>
        allStudents.value
            .filter(student =>
                student.firstAndLastName?.toLowerCase().includes(searchStudent.value?.toLowerCase() || "")
            ).map(student => student.firstAndLastName || "")
    );

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
        const parents = questionsTree.value
            .filter(question => question && typeof question === 'object' && !question.parentId);
        return parents;
    });

    function NextPage() {
        if (actualPageNumber.value < nbQuestionPage.value) {
            actualPageNumber.value++;
            insertTenRootQuestionInQuestionsToShow(actualPageNumber.value);
            scrollToDiv("searchDiv");
        }
    }

    function PreviousPage() {
        if (actualPageNumber.value > 1) {
            actualPageNumber.value--;
            insertTenRootQuestionInQuestionsToShow(actualPageNumber.value);
            scrollToDiv("searchDiv");
        }
    }

    function getDirectChildren(questionId) {
        const question = searchQuestionRecursif(questionsTree.value, questionId);

        if (undefined != question.children && question.children.length > 0)
            questionManagemnt(question)
    }

    function questionManagemnt(question) {
        if (question) {
            const questionToCloseOpen = questionsToShow.value.find(q => q.id === question.id);
            questionToCloseOpen.isOpen = !questionToCloseOpen.isOpen;
            const index = questionsToShow.value.findIndex(q => q.id === question.id);
            if (isThereAChildInQuestionsToShow(question))
                deleteChildren(question, index);
            else
                addChildren(question, index);
        }
    }

    function deleteChildren(question, index) {
        let nbQuestionsToClose = findNbChildrenToClose(question);
        const before = questionsToShow.value.slice(0, index + 1);
        const after = questionsToShow.value.slice(index + 1 + nbQuestionsToClose);
        questionsToShow.value = [...before, ...after];
    }

    function addChildren(question, index) {
        const before = questionsToShow.value.slice(0, index + 1);
        const after = questionsToShow.value.slice(index + 1);
        const newChildrenQuestion = question.children.map(child =>
            reactive({
                ...child,
                isOpen: false,
                parentId: question.id,
            })
        );
        questionsToShow.value = [...before, ...newChildrenQuestion, ...after];
    }

    function isThereAChildInQuestionsToShow(question) {
        const isThereAnChild = questionsToShow.value.findIndex(q => q.id === question.children[0].id);
        if (isThereAnChild == -1)
            return false;
        else
            return true;
    }

    function insertTenRootQuestionInQuestionsToShow(pageNumber) {
        questionsToShow.value = [];
        const start = (actualPageNumber.value - 1) * maxQuestionPerPage;
        const end = start + maxQuestionPerPage;
        questionsToShow.value = parentQuestions.value.slice(start, end);
    }

    function searchQuestionRecursif(questions, targetId) {
        for (const question of questions) {
            if (question.id === targetId)
                return question;
            if (question.children && question.children.length > 0) {
                const result = searchQuestionRecursif(question.children, targetId);
                if (result)
                    return result;
            }
        }
        return null;
    }

    function findNbChildrenToClose(question, nbQuestionsToClose = 0) {
        for (const child of question.children) {
            if (questionsToShow.value.findIndex(q => q.id === child.id) !== -1) {
                nbQuestionsToClose++;
                nbQuestionsToClose = findNbChildrenToClose(child, nbQuestionsToClose);
            }
        }
        return nbQuestionsToClose;
    }

    watch(searchQ, (newVal) => {
        const query = newVal.trim().toLowerCase();

        insertTenRootQuestionInQuestionsToShow(actualPageNumber.value);

        if (query)
            questionsToShow.value = questionsToShow.value.filter(q =>
                q.title.toLowerCase().includes(query)
            );
    });

    function scrollToDiv(elementName) {
        const element = document.getElementById(elementName);
        const topPos = element.offsetTop;

        window.scrollTo({
            top: topPos,
            behavior: "smooth"
        });
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
            background-color: #1967d2;
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

    .green_hover_bg:hover {
        background-color: #bbebbd !important;
        color: #333;
    }

    .scrollable-container {
        border: 2px solid black;
        background: white;
        width: 100%;
        left: 0;
        position: absolute;
        z-index: 1000;
        box-shadow: 0px 4px 6px rgba(0,0,0,0.1);
        max-height: 40vh;
        overflow-y: auto;
    }

    .scrollable-content {
        max-height: 80vh;
        overflow-y: auto;
        padding: 1em;
    }

    .scrollable-content {
        max-height: 80vh;
        overflow-y: auto;
        padding: 1em;
    }
</style>




