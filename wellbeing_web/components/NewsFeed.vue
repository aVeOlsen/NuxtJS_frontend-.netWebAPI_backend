<template>
  <div class="flex flex-col justify-center">
    <h1 class="justify-center flex my-4 pb-1 uppercase text-4xl border-b-2">
      Newsfeed
    </h1>
    <ul v-for="item in this.newsData" :key="item.id">
      <li>
        <div class="flex justify-center">
          <div class="border-b-2 p-6 border-gray-300">
            <nuxt-link :to="'/News/' + item.slug.current">
              <h1
                class="
                  uppercase
                  text-2xl
                  font-medium
                  text-blue-900
                  dark:text-blue-600
                  hover:underline
                ">
                {{ item.title }}
              </h1>
            </nuxt-link>
            <h2 class="text-xs mb-1">
              fra {{ item.startPeriod }} til {{ item.endPeriod }}
            </h2>
            <img
              style="width: 200px"
              class="mb-2 justify-center"
              :src="$urlFor(item.newscover)"
              :alt="item.title"
              v-if="item.newscover" />
            <p>{{ item.content.slice(0, 25) }}...</p>
          </div>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import { groq } from '@nuxtjs/sanity'
import sanityclient from '../sanityclient'

export default {
  props: {
    sanityQuery: '',
  },
  data() {
    return {
      newsData: [],
    }
  },

  mounted() {
    this.getNewsData(this.sanityQuery)
  },

  methods: {
    async getNewsData(sanityQuery) {
      sanityQuery = typeof sanityQuery !== 'undefined' ? sanityQuery : ''
      const query = groq`*[_type=="newsfeed"]${sanityQuery}{title, newscover, content, notes, slug, startPeriod, endPeriod}  | order(_createdAt asc)`
      const data = await sanityclient.fetch(query)
      this.newsData = data
      return this.newsData
    },
  },
}
</script>

<style></style>
