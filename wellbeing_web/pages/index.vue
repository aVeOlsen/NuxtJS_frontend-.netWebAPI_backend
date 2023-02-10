<template>
  <div class="text-2xl font-extrabold tracking-widest">
    <wellbeing-collector />
    <div v-if="showQuestionnaire">
      <div class="relative text-center">
        Nyt spørgeskema tilgængeligt
        <questionnaire-ID :id="questionID" />
      </div>
    </div>
    <img
      src="https://i.imgur.com/vexmkW9.png"
      alt=""
      style="width: 100%"
      class="img-fluid" />
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import WellbeingCollector from '../components/WellbeingCollector.vue'
import QuestionnaireID from '../components/QuestionnaireID.vue'
export default {
  name: 'IndexPage',
  components: { WellbeingCollector, QuestionnaireID },
  data() {
    return {
      showQuestionnaire: false,
      questionData: [],
      questionID: '',
      user: null,
    }
  },
  beforeMounted() {
    // setTimeout(() => {
      this.checkUser()
    // }, 350)
  },
  methods: {
    async checkUser() {
      if (this.isAuthenticated) {
        if (this.loggedInUser.departmentTitle) {
          this.checkQuestionnaire()
        } else {
          if (this.loggedInUser.departmentTitle) {
            this.checkQuestionnaire
          } else {
            this.user = await this.$auth.$storage.getLocalStorage('user')
            this.user = JSON.parse(JSON.stringify(this.user))
            this.checkUserQuestionnaire(this.user)
          }
        }
      }
    },
    async checkQuestionnaire() {
      await this.$axios
        .$get('/wellbeing/user/' + this.loggedInUser.id)
        .then((response) => {
          response.forEach((element) => {
            if (element.userID == this.loggedInUser.id) {
              if (element.subsections.length <= 1) {
                this.$store.dispatch('setWellbeingId', element.id)
                this.showQuestionnaire = true
                this.questionID = element.questionId
              }
            } else this.showQuestionnaire = false
          })
        })
    },
    async checkUserQuestionnaire(user) {
      await this.$axios.$get('/wellbeing/user/' + user.id).then((response) => {
        response.forEach((element) => {
          if (element.userID == user.id) {
            if (element.subsections.length <= 1) {
              this.$store.dispatch('setWellbeingId', element.id)
              this.showQuestionnaire = true
              this.questionID = element.questionId
            }
          } else this.showQuestionnaire = false
        })
      })
    },
  },
  computed: {
    ...mapGetters(['isAuthenticated', 'loggedInUser']),
  },
}
</script>
