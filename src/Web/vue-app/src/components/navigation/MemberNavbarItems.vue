<template>
  <ul class="navbar__nav">
    <li>
      <RouterLink :to="t('routes.account.path')" class="navbar__navlink">
        {{ t('routes.account.name') }}
      </RouterLink>
    </li>
    <li>
      <RouterLink :to="t('routes.studentProfile.path')" class="navbar__navlink">
        {{ t('routes.studentProfile.name') }}
      </RouterLink>
    </li>
    <RouterLink :to="t('routes.students.path')" class="navbar__navlink">
      {{ t('routes.students.name') }}
    </RouterLink>
    <RouterLink :to="t('routes.answers.path')" class="navbar__navlink">
      {{ t('routes.answers.name') }}
    </RouterLink>
    <RouterLink :to="t('routes.analyseCsv.path')" class="navbar__navlink">
      {{ t('routes.analyseCsv.name') }}
    </RouterLink>

  </ul>
</template>

<script lang="ts" setup>
import { useI18n } from "vue3-i18n";

const {t} = useI18n()
</script>
