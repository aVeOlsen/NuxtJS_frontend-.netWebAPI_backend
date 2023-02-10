<template>
  <section
    class="
      h-screen
      w-full
      flex flex-col
      justify-center
      items-center
      bg-[#1A2238]
    "
  >
    <div v-if="error.statusCode === 404">
      <h1 class="text-9xl font-extrabold text-white tracking-widest">404</h1>
      <div class="bg-[#FF6A3D] px-2 text-sm rounded rotate-12 absolute">
        Page Not Found
      </div>
    </div>
    <div v-if="error.statusCode===503">
      <div class="bg-[#FF6A3D] px-2 text-sm rounded absolute">
        <h1 class="text-9xl font-extrabold text-white tracking-widest">
          Vi er straks tilbage
        </h1>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  props: ['error'],
}
</script>

<style>
</style>