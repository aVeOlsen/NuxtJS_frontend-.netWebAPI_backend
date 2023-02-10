<template>
  <div>
    <div
      class="w-full md:w-96 md:max-w-full mx-auto mt-4 mb-4"
      style="min-width: 35rem">
      <div
        class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded inline-flex mb-2"
        @click="$router.back()">
        <button class="btn btn-blue">Tilbage</button>
      </div>
      <div class="p-6 border border-gray-300 sm:rounded-md">
        <h2
          class="font-bold text-center text-2xl"
          ref="newsTitle"
          :textContent="data.title"
          :title="data.title">
          {{ data.title }}
        </h2>
        <h2 class="text-xs mb-1">Periode start: {{ data.startPeriod }}</h2>
        <h2 class="text-xs mb-1">Periode slut: {{ data.endPeriod }}</h2>
        <div class="flex justify-center">
          <img
            class="mb-2 min-w-full"
            :src="$urlFor(data.newscover)"
            :alt="data.title"
            v-if="data.newscover" />
        </div>
        <p>
          {{ data.content }}
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import { groq } from '@nuxtjs/sanity'

export default {
  async asyncData({ params, $sanity }) {
    const query = groq`*[_type == "newsfeed" && slug.current == "${params.slug}"][0]`
    const data = await $sanity.fetch(query)
    return { data }
  },
}
</script>

<style></style>
