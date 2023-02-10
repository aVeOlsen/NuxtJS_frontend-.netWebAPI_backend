<template>
  <div>
    <div
      class="w-full md:w-96 md:max-w-full mx-auto mt-4 mb-4"
      style="min-width: 35rem">
      <div class="p-6 border border-gray-300 sm:rounded-md">
        <questionnaire-overview :sanityQuery="query" />
      </div>
    </div>
    <div class="my-4 flex justify-center">
      <div
        class="p-6 border border-gray-300 sm:rounded-md"
        style="min-width: 35rem">
        <news-feed :sanityQuery="query" />
      </div>
    </div>

    <!-- spacer -->

    <div class="my-4 flex justify-center">
      <div
        class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 pb-3 px-16 rounded text-center text-2xl">
        <button class="btn btn-blue right-0">
          <a :href="this.sanityUrl"> Opret nyt </a>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import QuestionnaireOverview from '../components/QuestionnaireOverview.vue'
import NewsFeed from '../components/NewsFeed.vue'

export default {
  middleware: 'isadmin',
  components: { QuestionnaireOverview, NewsFeed },
  data() {
    return {
      sanityUrl: process.env.sanityUrl,
      query: '[0..2]',
    }
  },
}
</script>

<style></style>
