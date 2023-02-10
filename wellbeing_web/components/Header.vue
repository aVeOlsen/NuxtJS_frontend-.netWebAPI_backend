<template>
  <nav
    class="flex flex-col items-center text-center sm:flex-row sm:text-left sm:justify-between py-4 px-6 bg-gray-800 shadow w-full">
    <div class="flex mb-2 sm:mb-0">
      <NuxtLink
        to="/"
        class="flex text-2xl no-underline text-white hover:text-blue-dark">
        <img
          src="https://i.imgur.com/EOGFSjZ.png"
          class="flex"
          style="width: 40px"
          alt="OnlinePlus logo" />
        <h3 class="text-2xl no-underline mt-1 text-white hover:text-blue-dark">
          OnlinePlus
        </h3>
      </NuxtLink>
    </div>

    <div class="flex font-medium">
      <NuxtLink
        to="/"
        class="text-lg no-underline text-white hover:text-blue-dark ml-4">
        <h6>Hjem</h6>
      </NuxtLink>

      <NuxtLink
        to="/MyPage"
        class="text-lg no-underline text-white hover:text-blue-dark ml-4">
        <h6>Min afdeling</h6>
      </NuxtLink>

      <!-- <NuxtLink
        to="/Contact"
        class="text-lg no-underline text-white hover:text-blue-dark ml-4">
        <h6>Kontakt</h6>
      </NuxtLink> -->

      <NuxtLink v-if="this.$auth.hasScope('admin')||this.$auth.hasScope('manager')"
        to="/adminOverview"
        class="text-lg no-underline text-white hover:text-blue-dark ml-4">
        <h6>Admin</h6>
      </NuxtLink>

      <!-- <NuxtLink
        to="/Messages"
        class="text-lg no-underline text-white hover:text-blue-dark ml-4">
        <h6>Beskeder</h6>
      </NuxtLink>

      <NuxtLink
        to="/Documents"
        class="text-lg no-underline text-white hover:text-blue-dark ml-4">
        <h6>Dokumenter</h6>
      </NuxtLink>

      <NuxtLink
        to="/Settings"
        class="text-lg no-underline text-white hover:text-blue-dark ml-4">
        <h6>Indstillinger</h6>
      </NuxtLink>

      <NuxtLink
        to="/Notifications"
        class="text-lg no-underline text-white hover:text-blue-dark ml-4">
        <h6>Notifikationer</h6>
      </NuxtLink> -->

      <!-- <NuxtLink
        to="/Login"
        class="text-lg no-underline text-white hover:text-blue-dark ml-14">
        <h6>Login</h6>
      </NuxtLink> -->

      <NavbarLogin
        class="text-lg no-underline text-white hover:text-blue-dark ml-14" />
    </div>
  </nav>
</template>

<script>
import NavbarLogin from './NavbarLogin.vue'

export default {
  components: {
    NavbarLogin,
  },
}
</script>

<style></style>
