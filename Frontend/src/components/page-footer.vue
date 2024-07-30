<template>
  <v-footer
    class="d-flex flex-column bottom-0 w-100 position-relative sm:d-block"
    style="padding: 0; bottom: 0px"
  >
    <div class="bg-yellow-lighten-1 w-100 d-flex justify-center">
      <v-btn
        v-for="icon in icons"
        :key="icon"
        :icon="icon"
        class="mx-4"
        size="small"
        variant="plain"
        @click="navigateToSocialMedia(icon)"
      ></v-btn>
    </div>

    <div class="px-4 py-2 bg-grey-darken-4 text-center w-100">
      {{ new Date().getFullYear() }} - <strong>Rose Design</strong>
    </div>
  </v-footer>
</template>

<script>
export default {
  data: () => ({
    icons: ["mdi-facebook", "mdi-twitter", "mdi-linkedin", "mdi-instagram"],
  }),
  methods: {
    navigateToSocialMedia(icon) {
      let socialMediaURL = "";

      switch (icon) {
        case "mdi-facebook":
          socialMediaURL = "https://www.google.com";
          break;
        case "mdi-twitter":
          socialMediaURL = "https://www.google.com";
          break;
        case "mdi-linkedin":
          socialMediaURL = "https://www.google.com";
          break;
        case "mdi-instagram":
          socialMediaURL = "https://www.google.com";
          break;
        default:
          socialMediaURL = "";
      }

      if (socialMediaURL) {
        window.open(socialMediaURL, "_blank");
      }
    },
  },
};
</script>

<style scoped>
/* @media (max-width: 600px) {
  .image-wrapper {
    width: 100%;
    height: auto;
  }
} */
</style>
