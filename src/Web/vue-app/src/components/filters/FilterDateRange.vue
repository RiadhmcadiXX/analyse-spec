<template>
    <div class="filter-date-range">
        <!-- Checkbox pour activer/désactiver le filtre -->
        <div class="checkbox-container">
            <input type="checkbox" id="checkbox-filter" v-model="isEnabled" @change="emitUpdate" />
            <label for="checkbox-filter" class="checkbox-label">{{ label }}</label>
        </div>

        <!-- Sélection du mode de filtre -->
        <div v-if="isEnabled" class="filter-options">
            <select v-model="mode" @change="emitUpdate">
                <option value="exact">Exacte</option>
                <option value="min">Minimum</option>
                <option value="max">Maximum</option>
                <option value="range">Plage</option>
            </select>

            <!-- Champ pour une seule date (Exacte, Min, début de Plage) -->
            <div v-if="mode !== 'max'">
                <label>
                    {{ mode === 'range' ? 'Début' : 'Date' }}
                    <input type="date" v-model="date1" @change="emitUpdate" />
                </label>
            </div>

            <!-- Champ pour la deuxième date (Fin de plage) -->
            <div v-if="mode === 'range'">
                <label>
                    Fin
                    <input type="date" v-model="date2" @change="emitUpdate" />
                </label>
            </div>
        </div>
    </div>
</template>

<script setup>
    /* eslint-disable */
    import { ref, watch, computed } from "vue";

    const props = defineProps({
        label: { type: String, default: "Date de naissance" },
        modelValue: { type: Object, required: true }
    });

    const emit = defineEmits(["update:modelValue"]);

    // Utilisation de `computed` pour synchroniser les valeurs correctement
    const isEnabled = computed({
        get: () => props.modelValue?.enabled ?? false,
        set: (val) => updateModel({ enabled: val })
    });

    const mode = computed({
        get: () => props.modelValue?._mode ?? "exact",
        set: (val) => updateModel({ _mode: val })
    });

    const date1 = computed({
        get: () => props.modelValue?._value1 ?? "",
        set: (val) => updateModel({ _value1: val })
    });

    const date2 = computed({
        get: () => props.modelValue?._value2 ?? "",
        set: (val) => updateModel({ _value2: val })
    });

    // Fonction pour mettre à jour le `v-model` proprement
    const updateModel = (updates) => {
        emit("update:modelValue", {
            enabled: isEnabled.value,
            _mode: mode.value,
            _value1: date1.value,
            _value2: date2.value,
            ...updates
        });
    };


</script>

<style scoped>
    .filter-date-range {
        display: flex;
        flex-direction: column;
        gap: 10px;
    }

    .filter-options {
        display: flex;
        flex-direction: column;
        gap: 5px;
        margin-left: 15px;
    }

    input[type="date"] {
        padding: 5px;
        border: 1px solid #63ae2b;
        border-radius: 5px;
    }

    input[type="checkbox"] {
        margin-right: 5px;
    }

    .checkbox-label {
        font-weight: bold;
        display: flex;
        align-items: center;
    }
</style>
