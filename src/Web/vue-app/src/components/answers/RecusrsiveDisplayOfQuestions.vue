<template>
    <div v-for="question in questionsToShow" :key="question.id" id="searchDiv">
        <div v-if="question.parentId == null">
            <div class="green_hover_bg" @click="toggleQuestion(question)" :style="{ padding: '1em', backgroundColor: '#F0FAFE', display: 'flex' }">
                <div v-if="question.children && question.children.length > 0 && !question.isOpen">
                    <p>&#11165;</p>
                </div>
                <div v-if="question.children && question.children.length > 0 && question.isOpen">
                    <p>&#11167;</p>
                </div>
                <strong>{{ question.title }}</strong>
            </div>
            <div v-for="(answer, index) in question.answers" :key="index">
                <div :style="{ padding: '1em', backgroundColor: '#ffffff' }">
                    &nbsp;&nbsp;&nbsp;&nbsp;{{ answer }}
                </div>
            </div>
        </div>
        <div v-else>
            <div class="green_hover_bg" @click="toggleQuestion(question)" :style="{ padding: '1em', backgroundColor: '#F0FAFE', display: 'flex' }">
                <div v-if="question.children && question.children.length > 0 && !question.isOpen">
                    <p>&#11165;</p>
                </div>
                <div v-if="question.children && question.children.length > 0 && question.isOpen">
                    <p>&#11167;</p>
                </div>
                <strong style="text-indent: 5vw;">{{ question.title }}</strong>
            </div>
            <div v-for="(answer, index) in question.answers" :key="index">
                <div :style="{ padding: '1em', backgroundColor: '#ffffff' }">
                    <p style="text-indent: 6vw;">{{ answer }}</p>
                </div>
            </div>
        </div>

        <RecusrsiveDisplayOfQuestions v-if="question.children && question.children.length && question.isOpen" :questionsToShow="question.children" />
    </div>
</template>

<script>
export default {
name: "RecusrsiveDisplayOfQuestions",
  props: {
    questionsToShow: {
      type: Array,
      default: () => []
    }
  },
  methods: {
    toggleQuestion(question) {
      question.isOpen = !question.isOpen;
    }
  }
};
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

    .fade-enter, .fade-leave-to {
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
</style>
