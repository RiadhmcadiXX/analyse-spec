<template>
  <div class="min-h-screen bg-gray-50 py-12 px-4">
    <div class="max-w-6xl mx-auto space-y-6">
      <div class="flex justify-end mb-4">
        <button
            @click="reloadAnalyse"
            class="btn"
        >
          Analyser encore
        </button>

      </div>


      <div class="splitter-sidebar">


        <div class="splitter-content">
          <!-- Carte d'importation -->
          <div class="container" v-if="!result && !isLoading">
            <Card class="upload-card">
              <h2 class="title">📁 Analyse d’un fichier CSV</h2>

              

              <!-- Sinon : formulaire de sélection -->
              <div>
                <!-- Sélection du fichier -->
                <input
                    type="file"
                    accept=".xlsx"
                    @change="e => selectedFile = e.target.files[0]"
                    class="file-input"
                />

                <!-- Bouton d'envoi -->
                <button
                    :disabled="!selectedFile"
                    @click="handleSubmit"
                    class="btn"
                >
                  Analyser
                </button>
              </div>
            </Card>
          </div>
          <div>
            <transition name="fade">
              <div v-if="isLoading" class="loading-overlay">
                <img :src="lottieAnim" alt="Chargement..." class="loading-image" />
                <p class="loading-text">📊 Lecture du fichier en cours...</p>
              </div>
            </transition>
            
          </div>

          
          
          <!-- 📛 Cours non réussis -->
          <div v-if="result?.coursNonReussis?.length" class="space-y-2">
            <h3 class="text-red-700 font-semibold text-md mt-4">Cours non réussis ❌ </h3>
            <div class="cours-container">
              <CoursCard
                  v-for="cours in coursNonReussisFiltres"
                  :key="cours.code"
                  :cours="cours"
                  :failed="true"
              />
            </div>
          </div>

          <hr class="my-6 border-t border-gray-300" />


          <!-- 📚 Cours réussis par catégorie -->
          <div v-if="result?.coursReussisParCategorie" class="space-y-6">
            <template v-for="(coursList, categorie) in result.coursReussisParCategorie" :key="categorie">
            <div>
              <h3 class="text-blue-700 font-semibold text-md capitalize">
                {{ formatCategorie(categorie) }}
              </h3>

              <!-- Si coursND => collapsibles -->
              <div v-if="categorie === 'coursND'" class="space-y-4 mt-2">
                <div v-for="(groupe, code) in coursNdParCode" :key="code">
                  <!-- Barre cliquable -->
                  <div @click="toggleCodeND(code)"
                      class="collapsible-header">
                    <span class="font-semibold text-gray-800">
                      {{ code }} ({{ groupe.length }} cours)
                    </span>
                    <span class="transform transition-transform duration-300"  :class="expandedCodesND.includes(code) ? 'rotate-90' : ''"> ▶</span>
                  </div >

                  <!-- Contenu des cours -->
                  <transition name="fade-expand">
                    <div v-if="expandedCodesND.includes(code)" class="pl-4 pt-2">
                      <div class="cours-container">
                        <CoursCard
                            v-for="cours in groupe"
                            :key="cours.code + cours.titre"
                            :cours="cours"
                        />
                      </div>
                    </div>
                  </transition>
                </div>
              </div>



              <!-- Sinon : affichage normal -->
              <div v-else class="cours-container-with-summary">
                <div class="cours-grid">
                  <CoursCard
                      v-for="cours in coursList"
                      :key="cours.code"
                      :cours="cours"
                  />
                </div>

                <CategorieSummaryCard
                    class="summary-fixed-right"
                    :label="formatCategorie(categorie)"
                    :current="coursList.length"
                    :total="objectifPourCategorie(categorie)"
                />
              </div>

              <hr class="my-6 border-t border-gray-300" />
            </div>
          </template>

          </div>
          
          
        </div>
        
        
      </div>
      
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, ref, watch } from "vue";
import Card from "@/components/layouts/items/Card.vue"
// composant à créer
import { useI18n } from 'vue3-i18n'
import { useAnalyseService } from "@/inversify.config"
import { IAnalyseCsvRequest } from "@/types/requests/analyseCsvRequest"
import { SucceededOrNotResponse } from "@/types/responses"
import { notifyError, notifySuccess } from "@/notify"
import CoursCard from "@/components/cards/CoursCard.vue";
import FilterSelect from "@/components/filters/FilterSelect.vue";
import Vue3Lottie from 'vue3-lottie'

import lottieAnim from '@/assets/animations/upload2.gif'

import { DotLottieVue } from '@lottiefiles/dotlottie-vue'
import CategorieSummaryCard from "@/components/cards/CategorieSummaryCard.vue";
import {useRouter} from "vue-router";


const router = useRouter()


function reloadAnalyse() {
  router.replace({ path: '/temp' }) // on redirige ailleurs temporairement
  setTimeout(() => {
    router.replace({ path: '/csv-analyse' }) // puis on revient
  }, 50)
}


const isLoading = ref(false)

const progress = ref(0) // pour la barre de progression (entre 0 et 100)


const { t } = useI18n()
const analyseService = useAnalyseService()
const selectedFile = ref<File | null>(null)
const result = ref<any>(null)
// const isLoading = ref(false)
async function handleSubmit() {
  if (!selectedFile.value) return
  
  isLoading.value = true

  progress.value = 0
  // Simulation de progression : 0% → 90%
  const interval = setInterval(() => {
    if (progress.value < 90) {
      progress.value += 10
    }
  }, 200)

  // Simule un "temps de lecture" (1.5 secondes)
  await new Promise(resolve => setTimeout(resolve, 600))

  clearInterval(interval)
  progress.value = 100
  
  const request: IAnalyseCsvRequest = { file: selectedFile.value }  //On passe le fichier ici avec l'interface --------------

  const response: SucceededOrNotResponse = await analyseService.analyseCsv(request)
  //isLoading.value = false

  // petit délai avant d’afficher le résultat
  setTimeout(() => {
    isLoading.value = false
    progress.value = 0
  }, 600)

  if (response.succeeded) {
    result.value = response.data

    const message = `${t('validation.csvAnalyse.success')} Fichier chargé`
    notifySuccess(message)
  } else {
    const errors = response.getErrorMessages('validation.csvAnalyse')
    notifyError(errors.length ? errors[0] : t('validation.csvAnalyse.errorOccured'))
  }
}

// Utilitaire : reformate "litterature601" en "Littérature (601)"
function formatCategorie(categorie: string): string {
  const labels: Record<string, string> = {
    litterature601: "📘 Littérature (601)",
    philosophie340: "📗 Philosophie (340)",
    educationPhysique109: "🏃‍♂️ Éducation physique (109)",
    anglais604: "📙 Anglais (604)",
    coursND: "📒 Cours ND / Complémentaires"
  }
  return labels[categorie] ?? categorie
}



// Filtre par code de cours
const codeFilter = ref({ enabled: false, value: "" })

const codeOptions = [
  { value: "601", text: "Littérature (601)" },
  { value: "340", text: "Philosophie (340)" },
  { value: "109", text: "Éducation Physique (109)" },
  { value: "604", text: "Anglais (604)" },
  { value: "ND",  text: "Cours ND" }
]


const coursNonReussisFiltres = computed(() => {
  if (!result.value?.coursNonReussis) return []

  const cours = result.value.coursNonReussis

  if (codeFilter.value.enabled && codeFilter.value.value) {
    return cours.filter(c =>
        c.code?.startsWith(codeFilter.value.value)
    )
  }

  return cours
})


const coursNdParCode = computed(() => {
  const coursND = result.value?.coursReussisParCategorie?.coursND || []
  const groupes: Record<string, any[]> = {}

  for (const cours of coursND) {
    const prefix = cours.code?.substring(0, 3) || "Autre"

    if (!groupes[prefix]) {
      groupes[prefix] = []
    }
    groupes[prefix].push(cours)
  }

  return groupes
})


const expandedCodesND = ref<string[]>([])

function toggleCodeND(code: string) {
  if (expandedCodesND.value.includes(code)) {
    expandedCodesND.value = expandedCodesND.value.filter(c => c !== code)
  } else {
    expandedCodesND.value.push(code)
  }
}


function objectifPourCategorie(categorie: string): number {
  const objectifs: Record<string, number> = {
    litterature601: 4,
    philosophie340: 3,
    educationPhysique109: 3,
    anglais604: 2,
    coursND: 0, // optionnel, ND complémentaires
  }
  return objectifs[categorie] ?? 0
}




</script>

<style scoped>


img {
  max-width: 100%;
  height: auto;
}

.container {
  min-height: 80vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #f9fafb;
  padding: 1rem;
}

.animationContainer {
  min-height: 80vh;
  max-width: 850px;
  
}

.upload-card {
  max-width: 800px;
  width: 100%;
  padding: 2rem;
  border-radius: 1rem;
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.1);
  background-color: white;
}

.title {
  font-size: 1.25rem;
  font-weight: bold;
  margin-bottom: 1.5rem;
}

.file-input {
  width: 100%;
  padding: 0.75rem;
  margin-bottom: 1rem;
  border: 1px solid #ccc;
  border-radius: 0.5rem;
}


.cours-container {
  display: flex;
  flex-wrap: wrap;
  gap: 16px;
  margin-top: 24px;
  justify-content: flex-start;
}


.cours-container-with-summary {
  position: relative;
  margin-bottom: 3rem;
}

.cours-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 16px;
  padding-right: 180px; /* Pour éviter que les cards se cachent derrière le résumé */
}

.summary-fixed-right {
  position: absolute;
  top: 0;
  right: 0;
  z-index: 10;
}


h3
{
  margin-top: 8px;
}


 .splitter-sidebar {
   width: 100%;
   display: flex;
   gap: 1rem;
   align-items: flex-start;
 }

.splitter-content {
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}


form#dataset-filters {
  display: flex;
  flex-direction: column;
  align-items: stretch;
  gap: 1rem;
  flex-grow: 1;
  width: 25%;
}

fieldset {
  padding: 1rem;
  border: 1px solid black;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

fieldset > legend {
  font-weight: bold;
  padding-inline: 0.25rem;
}


.loading-overlay {
  position: fixed;
  inset: 0;
  z-index: 999;
  background-color: white;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.loading-image {
  width: 600px;
  height: 600px;
  object-fit: contain;
  margin-bottom: 20px;
}

.loading-text {
  font-size: 1.5rem;
  color: #1d4ed8; /* text-blue-700 */
  text-align: center;
  font-weight: 600;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity 0.5s ease;
}
.fade-enter-from, .fade-leave-to {
  opacity: 0;
}


.fade-expand-enter-active,
.fade-expand-leave-active {
  transition: all 0.3s ease;
  max-height: 1000px;
  opacity: 1;
}

.fade-expand-enter-from,
.fade-expand-leave-to {
  max-height: 0;
  opacity: 0;
  overflow: hidden;
}


.collapsible-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  cursor: pointer;
  padding: 1rem 1.5rem;
  background-color: white;
  border: 1px solid #e5e7eb; /* gray-200 */
  border-radius: 0.5rem;
  box-shadow: 0 1px 2px rgba(0, 0, 0, 0.05);
  //transition: all 0.2s ease-in-out;
  margin-top: 10px;
  margin-bottom: 10px;
}

.collapsible-header:hover {
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}



</style>
