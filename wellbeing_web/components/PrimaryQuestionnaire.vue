<template>
  <div>
    <Notification :message="this.msg" v-if="this.msg" />
    <h1 class="justify-center flex my-4 pb-1 uppercase text-4xl border-b-2">
      Trivsels undersøgelser
    </h1>
    <div class="flex justify-center">
      <select
        class="
          bg-blue-500
          hover:bg-blue-700
          text-white
          py-2
          px-4
          font-bold
          rounded
        "
        name="PrimaryQuestionnaire"
        id="Questionnaire">
        <option>Primary: {{ getQuestionTitle }}</option>
        <optgroup v-for="item in this.questionData" :key="item.id">
          <option :value="item.slug.current">{{ item.title }}</option>
        </optgroup>
      </select>
    </div>
    <div class="pb-6 border-b-2 border-gray-900 mt-4 flex justify-center">
      <div class="top-4">
        <div
          class="
            bg-blue-500
            hover:bg-blue-700
            text-white
            font-bold
            py-2
            px-4
            rounded-full
            inline-flex
          ">
          <button class="btn btn-blue" @click="setPrimary">
            Sæt primært spørgeskema.
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import Notification from './Notification.vue'
export default {
  components: { Notification },
  props: {
    questionData: [],
  },
  data() {
    return {
      msg: '',
    }
  },
  methods: {
    setPrimary() {
      var options = document.getElementsByTagName('option')
      for (let i = 0; i < options.length; i++) {
        const element = options[i]
        if (element.selected) {
          this.$axios
            .put('/wellbeing/quistionnaire/' + element.value.toString())
            .then(() => {
              this.$store.dispatch('setQuestionTitle', element.text)
            })
        }
      }
      this.msg = 'Primært spørgeskema blev sat.'
    },
  },
  computed: {
    ...mapGetters(['loggedInUser', 'getQuestionTitle']),
  },
}
</script>

<style>
</style>