<template>
  <section class="my-4 flex justify-center">
    <div
      class="
        mx-2
        flex
        max-w-sm
        p-6
        bg-white
        border border-gray-200
        rounded-lg
        shadow-md
        dark:bg-gray-800 dark:border-gray-700
      ">
      <div class="left-0 top-0" v-if="this.allResult">
        <h1
          class="
            text-center text-2xl
            font-bold
            tracking-tight
            text-gray-900
            dark:text-white
          ">
          Onlineplus samlet trivsel
        </h1>
        <h1
          class="
            text-center
            mb-2
            text-3xl
            font-bold
            tracking-tight
            text-gray-900
            dark:text-white
          ">
          {{ this.allResult }}
        </h1>
        <p
          class="
            font-normal
            text-xl text-center text-gray-700
            dark:text-gray-400
          ">
          Her kan du se den samlede trivsel for hele OnlinePlus.
        </p>
      </div>
    </div>

    <!-- spacer -->

    <div
      class="
        mx-2
        flex
        max-w-sm
        p-6
        bg-white
        border border-gray-200
        rounded-lg
        shadow-md
        dark:bg-gray-800 dark:border-gray-700
      ">
      <div class="top-0 right-0" v-if="this.departmentResult">
        <h1
          class="
            text-center text-2xl
            font-bold
            tracking-tight
            text-gray-900
            dark:text-white
          ">
          {{ this.loggedInUser.departmentTitle }} samlet trivsel
        </h1>
        <h1
          class="
            text-center
            mb-2
            text-3xl
            font-bold
            tracking-tight
            text-gray-900
            dark:text-white
          ">
          {{ this.departmentResult }}
        </h1>
        <p
          class="
            font-normal
            text-center text-xl text-gray-700
            dark:text-gray-400
          ">
          Her kan du se den samlede trivsel for din egen afdeling.
        </p>
      </div>
    </div>
  </section>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  data() {
    return {
      wellbeingLevel: [],
      allResult: null,
      departmentResult: null,
    }
  },

  async beforeMount() {
    setTimeout(async () => {
      await this.getAllWellbeingData()
      await this.getDepartmentData()
    }, 350)
  },
  watch:{
    '$route.query':'this.loggedInUser',
  },
  methods: {
    async getAllWellbeingData() {
      this.wellbeingLevel = []

      await this.$axios.$get('/wellbeing/').then((response) => {
      response.forEach((element) => {
        if (element.primary) {
          this.wellbeingLevel = this.wellbeingLevel.concat(
            element.wellbeingLevel
          )
          this.$store.dispatch('setQuestionTitle', element.title)
        }
      })
      this.wellbeingLevel.forEach((element) => {
        this.allResult += element
      })
      this.allResult /= this.wellbeingLevel.length
      this.allResult = this.allResult.toFixed(2)
      })
    },
    async getDepartmentData() {
      this.wellbeingLevel = []
      await this.$axios
        .$get('/wellbeing/department/' + this.loggedInUser.departmentTitle)
        .then((response) => {
          response.forEach((element) => {
            if (element.primary)
              this.wellbeingLevel = this.wellbeingLevel.concat(
                element.wellbeingLevel
              )
          })
          this.wellbeingLevel.forEach((element) => {
            this.departmentResult += element
          })
          this.departmentResult /= this.wellbeingLevel.length
          this.departmentResult = this.departmentResult.toFixed(2)
        })
    },
  },
  computed: {
    ...mapGetters(['isAuthenticated', 'loggedInUser']),
  },
}
</script>

<style></style>
