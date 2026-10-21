<template>
    <div>
        <h2>Affichage des questions principales</h2>

        <div v-for="(question, index) in parentQuestions" :key="question.id">
            <p :style="{ padding: '1em', backgroundColor: index % 2 === 0 ? '#dfdfdf' : '#ffffff' }">
                <strong>Question {{ index + 1 }}: {{ question.title }}</strong>
            </p>

            <!-- Affichage des options associées à la question -->
            <div v-for="(item, idx) in filteredOptions(question.id)" :key="item.idOption.id"
                 :style="{ padding: '1em', backgroundColor: idx % 2 === 0 ? '#e9e9e9' : '#ffffff' }">
                <strong>Option {{ idx + 1 }}:</strong> {{ item.idOption.title }}
            </div>

            <!-- Affichage des sous-questions automatiquement -->
            <div v-if="question.children.length > 0">
                <QuestionComponent :questions="question.children" :options="options" />
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            questions: {
                type: Array,
                required: true
            },
            options: {
                type: Array,
                required: true
            }
        },
        computed: {
            // Trouver les questions sans parent (questions principales)
            parentQuestions() {
                const parents = this.questions.filter(question => !question.parentId);
                console.log("Questions principales filtrées :", parents);
                return parents;
            }
        },
        methods: {
            // Filtrer les options en fonction de la question id
            filteredOptions(questionId) {
                const filtered = this.options.filter(option => option.idOption.idQuestion.id === questionId);
                console.log(`Options filtrées pour la question ${questionId} :`, filtered);
                return filtered;
            }
        },
        mounted() {
            console.log("Props reçues - Questions:", this.questions);
            console.log("Props reçues - Options:", this.options);
        }
    };
</script>
