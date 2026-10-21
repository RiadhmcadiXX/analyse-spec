<template>
    <input
        type="file"
        ref="fileInput"
        accept=".csv"
        @change="handleFileUpload"
        style="display: none"
    >
    <button class="btn" @click="triggerFileInput">Import CSV</button>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import {useSpecService} from "@/inversify.config";
import {ICreateSpecRequest} from "@/types/requests";

const fileInput = ref<HTMLInputElement | null>(null);
const specService = useSpecService();
//const specRequest = ref<ICreateSpecRequest>({FileData: ""});


const triggerFileInput = () => {
  if(fileInput.value) {
    fileInput.value.click()
  }
};

const handleFileUpload = async (event: any) => {
  const file = event.target.files[0];
  
  function readBrowserCsv(
      file: File,
      onSuccess: (result: string) => void,
      onError: (error: Error) => void
  ): void {
    const reader = new FileReader();

    reader.onload = (event) => {
      if (event.target?.result) {
        onSuccess(event.target.result as string);
      } else {
        onError(new Error('Failed to read file'));
      }
    };

    reader.onerror = () => {
      onError(new Error('File reading error'));
    };
    
    reader.readAsText(file);
  }
  
  readBrowserCsv(
      file,
      (result) => {
        const request: ICreateSpecRequest = {FileData: result}
        specService.createSpec(request)
      },
      (error) => console.error("Error:", error)
  );
};

</script>

