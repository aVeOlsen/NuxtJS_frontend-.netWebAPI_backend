<template>
  <div>
    <div v-if="msg">
      <notification :message="msg" />
    </div>
    <div v-for="item in this.questionData" :key="item.id">
      <nuxt-link :to="'/Questionnaire/' + item.slug.current">
        <br />
        <h2
          class="
            uppercase
            text-2xl
            font-medium
            text-blue-900
            dark:text-blue-600
            hover:underline
            text-center
          ">
          {{ item.title }}
        </h2></nuxt-link
      >
      <h6 class="text-center mb-2">{{ item.description }}</h6>
      <div class="flex justify-center">
        <img
          style="width: 200px"
          class="mb-2"
          :src="$urlFor(item.coverimage)"
          :alt="item.title"
          v-if="item.coverimage" />
      </div>
      <div class="flex mb-2 justify-center">
        <div class="checkbox bounce" v-if="loggedInUser.scope == 'admin'">
          <input
            type="checkbox"
            :name="item.slug.current"
            id="HR"
            :value="'HR'" />
          <label for="HR">HR</label>
          <input
            type="checkbox"
            :name="item.slug.current"
            id="SEO"
            :value="'SEO'" />
          <label for="SEO">SEO</label>
          <input
            type="checkbox"
            :name="item.slug.current"
            id="Udvikler"
            :value="'Udvikler'" />
          <label for="Udvikler">Udvikler</label>
          <input
            type="checkbox"
            :name="item.slug.current"
            id="SoMe"
            :value="'SoMe'" />
          <label for="SoMe">SoMe</label>
        </div>
      </div>
      <div class="checkbox bounce" v-if="loggedInUser.scope == 'manager'">
        <input
          type="checkbox"
          :name="item.slug.current"
          :id="loggedInUser.departmentTitle"
          :value="loggedInUser.departmentTitle.toString()" />
        <label :for="loggedInUser.departmentTitle">{{
          loggedInUser.departmentTitle
        }}</label>
      </div>
      <div class="flex justify-center">
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
          <button class="btn btn-blue" @click="getDepartment">
            Start trivsels undersøgelse
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
  components: {
    Notification,
  },
  props: {
    questionData: [],
  },
  data() {
    return {
      Wellbeing: {
        QuestionId: '',
        Title: '',
        Subsections: [],
        WellbeingLevel: [],
        DepartmentTitle: '',
      },
      msg: '',
    }
  },
  methods: {
    async getDepartment() {
      var inputs = document.getElementsByTagName('input')
      this.msg = 'Spørgeskema publiceres.'
      for (let i = 0; i < inputs.length; i++) {
        const element = inputs[i]
        if ((element.type = 'checkbox')) {
          if (element.checked) {
            await this.publish(element.value, element.name)
          }
        }
      }
    },
    async publish(value, name) {
      if (value && name) {
        this.Wellbeing.departmentTitle = value
        this.Wellbeing.QuestionID = name
        await fetch('/.netlify/functions/publishQuestionnaire', {
          method: 'POST',
          body: JSON.stringify(this.Wellbeing),
          headers: {
            Authorization: `Bearer ${this.loggedInUser.token}`
          }
        }).then((response) => {
          if (!response.ok) {
            throw new Error(response.statusText)
          }
        })
      this.msg = 'Spørgeskema blev publiceret'
      }
    },
  },
  computed: {
    ...mapGetters(['loggedInUser']),
  },
}
</script>

<style>
</style>