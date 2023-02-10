<template>
  <section>
    <div>
      <div class="flex justify-center mt-4">
        <div class="w-full max-w-xs">
          <h2 class="text-2xl text-center mt-2 mb-2 font-bold">
            Welcome back!
          </h2>
          <Notification :message="error" v-if="error" />
          <form
            class="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4"
            method="post"
            @submit.prevent="loginAsync">
            <div class="mb-4">
              <label class="block text-gray-700 text-sm font-bold mb-2"
                >Email</label
              >
              <div class="control">
                <input
                  type="email"
                  class="
                    shadow
                    appearance-none
                    border
                    rounded
                    w-full
                    py-2
                    px-3
                    text-gray-700
                    leading-tight
                    focus:outline-none focus:shadow-outline
                  "
                  name="email"
                  v-model="login.email" />
              </div>
            </div>
            <div class="mb-6">
              <label class="label font-semibold">Password</label>
              <div class="control">
                <input
                  type="password"
                  class="
                    shadow
                    appearance-none
                    border
                    rounded
                    w-full
                    py-2
                    px-3
                    text-gray-700
                    leading-tight
                    focus:outline-none focus:shadow-outline
                  "
                  minlength="4"
                  name="password"
                  v-model="login.password" />
              </div>
            </div>
            <div class="flex justify-center">
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
                type="submit">
                Log In
              </button>
            </div>
          </form>
          <!-- <div class="has-text-centered" style="margin-top: 20px">
            <p>
              Don't have an account?
              <nuxt-link to="/register">Register</nuxt-link>
            </p>
          </div> -->
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { mapMutations } from 'vuex'
import Notification from '../components/Notification.vue'
export default {
  auth: false,
  'auth-user': false,
  middleware: false,
  components: {
    Notification,
  },

  data() {
    return {
      login: {
        email: '',
        password: '',
      },
      error: null,
    }
  },

  methods: {
    async loginAsync() {
      try {
        this.login.password = this.$aesEncrypt(this.login.password)
        await this.$auth
          .loginWith('local', {
            data: this.login,
          })
          .then((response) => {
            this.$auth.setUserToken(response.data.appUser.token)
            this.$axios
              .$get('/AppUser/' + response.data.appUser.id)
              .then((response) => {
                const user = response
                if (user.role == 0) {
                  user['scope'] = 'admin'
                } else if (user.role == 1) {
                  user['scope'] = 'manager'
                }
                this.$auth.$storage.setUniversal('user', user, true) //begge er ikke nødvendige ( denne fra nuxt module, den anden under med vuex)
                // this.$store.commit('setUser', user)
              })
          })
        this.$router.push('/')
      } catch (e) {
        this.error = e.response.data.message
        console.log(this.error)
      }
    },
  },
  computed: {
    ...mapMutations(['setUser']),
  },
}
</script>

<style></style>
