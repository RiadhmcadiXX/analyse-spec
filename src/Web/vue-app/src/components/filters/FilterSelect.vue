<template>
    <div class="filter-select">
        <!-- Checkbox pour activer/désactiver le filtre -->
        <div class="checkbox-container">
            <input type="checkbox" :id="checkboxId" v-model="isEnabled" @change="updateFilter" />
            <label :for="checkboxId" class="checkbox-label">{{ label }}</label>
        </div>

        <!-- Select qui s'affiche uniquement si le filtre est activé -->
        <select v-if="isEnabled" v-model="selectedValue" @change="updateFilter">
            <option v-for="option in options" :key="option.value" :value="option.value">
                {{ option.text }}
            </option>
        </select>
    </div>
</template>

<script setup>
    /* eslint-disable */
    import { computed, ref, watch } from "vue";

    // Déclaration des props
    const props = defineProps({
        label: { type: String, required: true },
        options: { type: Array, required: true }, // [{ value: "M", text: "M" }, { value: "F", text: "F" }]
        modelValue: { type: Object, required: true } // { enabled: false, value: "" }
    });

    // Déclaration des événements
    const emit = defineEmits(["update:modelValue"]);

    // ID unique pour chaque checkbox
    const checkboxId = ref(`checkbox-${Math.random().toString(36).substr(2, 9)}`);

    // Gestion de l'état du checkbox et du select avec `computed`
    const isEnabled = computed({
        get: () => props.modelValue.enabled,
        set: (newValue) => {
            emit("update:modelValue", { ...props.modelValue, enabled: newValue });
        }
    });

    const selectedValue = computed({
        get: () => props.modelValue.value,
        set: (newValue) => {
            emit("update:modelValue", { ...props.modelValue, value: newValue });
        }
    });

    // Fonction pour émettre la mise à jour du filtre
    const updateFilter = () => {
        if (props.modelValue.enabled !== isEnabled.value || props.modelValue.value !== selectedValue.value) {
            emit("update:modelValue", {
                enabled: isEnabled.value,
                value: selectedValue.value
            });
        }
    };

    // Synchronisation si `modelValue` change depuis le parent
    watch(() => props.modelValue, (newValue) => {
        isEnabled.value = newValue.enabled;
        selectedValue.value = newValue.value;
    });
</script>

<style scoped>
    .filter-select {
        display: flex;
        flex-direction: column;
        gap: 8px;
        
    }

    /* Aligner proprement le checkbox et son texte */
    .checkbox-container {
        display: flex;
        align-items: center;
        gap: 8px;
    }

    /* Style du checkbox */
    input[type="checkbox"] {
        width: 16px;
        height: 16px;
        cursor: pointer;
    }

    /* Rendre le texte cliquable */
    .checkbox-label {
        font-size: 16px;
        cursor: pointer;
        font-weight: bold;
    }

    /* Style du select */
    select {
        padding: 6px;
        border: 1px solid #63ae2b;
        border-radius: 5px;
        background-color: #fff;
        cursor: pointer;
    }
</style>
