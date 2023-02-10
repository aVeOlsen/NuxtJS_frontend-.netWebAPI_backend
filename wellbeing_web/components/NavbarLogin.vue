<template>
  <nav class="navbar is-light">
    <div class="container">
      <div class="navbar-brand"></div>
      <div class="navbar-menu">
        <div class="navbar-end">
          <div
            class="navbar-item has-dropdown is-hoverable"
            v-if="isAuthenticated">
            <a class="navbar-link">
              <!-- {{ loggedInUser.email }} -->
            </a>
            <div class="navbar-dropdown" v-if="loggedInUser">
              <nuxt-link class="navbar-item" to="/profile"
                >Navn: {{ this.loggedInUser.firstName }}
              </nuxt-link>

              <!-- >{{loggedInUser.email}}</nuxt-link -->
              <a class="navbar-item ml-4" @click="logout">Logout</a>
            </div>
          </div>
          <template v-else>
            <nuxt-link class="navbar-item" to="/register">Register</nuxt-link>
            <nuxt-link class="navbar-item ml-4" to="/login">Log In</nuxt-link>
          </template>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  auth: false,
  data() {
    return { user: {} }
  },

  // Look into refactor with using mounted store.
  async mounted() {
    if (this.isAuthenticated) {
      this.user = await this.$auth.$storage.getLocalStorage('user')
      this.user = JSON.parse(JSON.stringify(this.user))
      // this.$store.commit('setUser', this.user) //begge er ikke nødvendige.(denne og under)
      this.$auth.$storage.setUniversal('user', this.user, true)
    }
  },
  // You create the computed properties by using the spread operator (...) to extract the getters from mapGetters.
  computed: {
    ...mapGetters(['isAuthenticated', 'loggedInUser']),
  },

  methods: {
    async logout() {
      await this.$auth.$storage.removeUniversal('user')
      await this.$auth.logout()
      this.$router.push('login')
    },
  },
}
</script>

<style></style>
