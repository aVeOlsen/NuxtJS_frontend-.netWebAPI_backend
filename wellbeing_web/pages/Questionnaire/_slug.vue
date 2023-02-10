<template>
  <div
    class="w-full md:w-96 md:max-w-full mx-auto mt-4 mb-4"
    style="min-width: 35rem">
    <div
      class="
        bg-blue-500
        hover:bg-blue-700
        text-white
        font-bold
        py-2
        px-4
        rounded
        inline-flex
        mb-2
      "
      @click="$router.back()">
      <button class="btn btn-blue">Tilbage</button>
    </div>
    <div class="p-6 border border-gray-300 sm:rounded-md">
      <div>
        <h2
          class="font-bold text-center text-2xl"
          ref="wellbeingTitle"
          :textContent="data.title"
          :title="data.title">
          {{ data.title }}
        </h2>
        <h6 class="text-center mb-2">{{ data.description }}</h6>
        <div class="flex justify-center">
          <img
            style="width: 200px"
            class="mb-2"
            :src="$urlFor(data.coverimage)"
            :alt="data.title"
            v-if="data.coverimage" />
        </div>
      </div>
      <div v-if="data.questionList.length >= 1">
        <div v-for="item in data.questionList" :key="item.id">
          <div class="mb-1 flex ml-8">
            <label class="w-3/5 font-medium" :for="item">
              {{ item }}
            </label>
            <div class="w-2/5 font-medium">
              <label class="mr-2">
                1<input class="ml-1" type="radio" :name="item" :value="1"
              /></label>
              <label class="mr-2">
                2<input class="ml-1" type="radio" :name="item" :value="2"
              /></label>
              <label class="mr-2">
                3<input class="ml-1" type="radio" :name="item" :value="3"
              /></label>
              <label class="mr-2">
                4<input class="ml-1" type="radio" :name="item" :value="4"
              /></label>
              <label class="mr-2">
                5<input class="ml-1" type="radio" :name="item" :value="5"
              /></label>
            </div>
          </div>
        </div>
      </div>
      <Notification class="bg-blue-300" :message="this.msg" v-if="this.msg" />
      <div class="flex justify-center mt-5">
        <button
          class="
            bg-blue-500
            hover:bg-blue-700
            text-white
            font-bold
            py-2
            px-4
            rounded
          "
          @click="this.getData">
          Submit
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import { groq } from '@nuxtjs/sanity'
import Notification from '../../components/Notification.vue'
import { mapGetters } from 'vuex'
export default {
  data() {
    return {
      wellbeing: {
        title: '',
        subsections: [],
        wellbeingLevel: [],
        userId: '',
        departmentTitle: '',
        questionId: '',
        primary: false,
        wellbeingID: '',
      },
      msg: '',
      checkName: '',
      // wellbeingID: this.getWellbeingId,
    }
  },
  components: { Notification },
  methods: {
    getData() {
      var inputs = document.getElementsByTagName('input')
      for (let i = 0; i < inputs.length; i++) {
        const element = inputs[i]
        if ((element.type = 'radio')) {
          if (element.checked) {
            this.wellbeing.wellbeingLevel.push(element.value)
          }
          if (element.name != this.checkName) {
            this.checkName = element.name
            this.wellbeing.subsections.push(element.name)
          }
        }
      }
      if (
        this.wellbeing.subsections.length ==
        this.wellbeing.wellbeingLevel.length
      ) {
        this.sendData()
      } else {
        this.wellbeing.subsections = []
        this.wellbeing.wellbeingLevel = []
        this.msg = 'Please check every section, before submit.'
      }
    },
    async sendData() {
      if (this.$auth.user.id) {
        this.wellbeing = {
          title: this.data.title,
          userId: this.loggedInUser.id,
          departmentTitle: this.loggedInUser.departmentTitle,
          questionId: this.data.slug.current,
          subsections: this.wellbeing.subsections,
          wellbeingLevel: this.wellbeing.wellbeingLevel,
          wellbeingId: this.getWellbeingId,
        }
        if (this.getQuestionTitle == this.data.title) {
          this.wellbeing.primary = true
        }
        await fetch('/.netlify/functions/updateWellbeing', {
          method: 'PUT',
          body: JSON.stringify(this.wellbeing),
          headers: {
            Authorization: `Bearer ${this.$auth.user.token}`,
          },
        }).then(() => {
          alert('Thank you for your participation')
          this.$router.push('/')
        })
      }
    },
  },
  async asyncData({ params, $sanity }) {
    const query = groq`*[_type == "questionnaire" && slug.current == "${params.slug}"][0]`
    const data = await $sanity.fetch(query)
    return { data }
  },
  computed: {
    ...mapGetters(['getWellbeingId', 'loggedInUser', 'getQuestionTitle']),
  },
}
</script>
