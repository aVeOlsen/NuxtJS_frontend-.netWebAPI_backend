<template>
  <div class="text-center items-center rounded bg-gray-800 text-white text-sm font-bold px-4 py-2">
    {{ message }}
  </div>
</template>

<script>
export default {
  name: 'Notification',
  props: ['message']
}
</script>
