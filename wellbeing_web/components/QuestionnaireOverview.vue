<template>
  <section v-if="this.questionData.length > 0">
    <primary-questionnaire :questionData="this.questionData" />
    <publish-questionnaire :questionData="this.questionData"/>
  </section>
</template>

<script>
import { groq } from '@nuxtjs/sanity'
import sanityclient from '../sanityclient'
import PrimaryQuestionnaire from './PrimaryQuestionnaire.vue'
import PublishQuestionnaire from './PublishQuestionnaire.vue'
export default {
  components: {
    PrimaryQuestionnaire,
    PublishQuestionnaire
  },
  props: {
    sanityQuery: '',
  },
  data() {
    return {
      questionData: [],
    }
  },
  mounted() {
    this.getQuestionData(this.sanityQuery)
  },
  methods: {
    async getQuestionData(sanityQuery) {
      sanityQuery = typeof sanityQuery !== 'undefined' ? sanityQuery : ''
      var query = groq`*[_type=="questionnaire"]${sanityQuery}{title, description, coverimage, slug} | order(_createdAt desc)`
      const data = await sanityclient.fetch(query)
      this.questionData = data
      return this.questionData
    },
  },
}
</script>

<style></style>
