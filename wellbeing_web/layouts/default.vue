<template>
  <div>
    <Header class="sticky top-0 z-50" />
    <nuxt />
    <Footer />
  </div>
</template>

<script>
import Header from '../components/Header.vue'
import Footer from '../components/Footer.vue'
export default {
  components: {
    Header,
    Footer,
  },
}
</script>

<style></style>
