<template>
  <div>
    <div
      v-for="item in questionData"
      :key="item.id"
      class="bg-blue-500 hover:bg-blue-700 text-white font-bold px-4 rounded-full inline-flex right-0">
      <button class="btn btn-green">
        <nuxt-link :to="'questionnaire/'+item.slug.current"
          >Gå til spørgeskema: {{ item.title }}
        </nuxt-link>
      </button>
      <img :src="$urlFor(item.coverimage)" class="mb-2 w-24" />
    </div>
  </div>
</template>

<script>
import { groq } from '@nuxtjs/sanity'
import sanityclient from '../sanityclient'
export default {
  props: {
    id: '',
  },
  data() {
    return {
      questionData: [],
    }
  },
  mounted() {
    this.getQuestionnaire(this.id)
  },
  methods: {
    async getQuestionnaire(questionId) {
      const query = groq`*[_type=="questionnaire" && slug.current == "${questionId}"]{title, description, coverimage, slug, _id}`
      this.questionData = await sanityclient.fetch(query)
    },
  },
}
</script>

<style></style>
