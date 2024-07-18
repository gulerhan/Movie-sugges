<template>
  <v-footer class="d-flex flex-column bottom-0 w-100 position-relative  " style="padding: 0; bottom: 0px;">
    <div class="bg-yellow-lighten-1 d-flex w-100 align-center px-4">
      <strong>Sosyal medya hesaplarımızı takip etmek için</strong>

      <v-spacer></v-spacer>

      <v-btn
        v-for="icon in icons"
        :key="icon"
        :icon="icon"
        class="mx-4 "
        size="small"
        variant="plain"
        @click="navigateToSocialMedia(icon)"
      ></v-btn>
    </div>

    <div class="px-4 py-2 bg-grey-darken-4 text-center w-100">
      {{ new Date().getFullYear() }} - <strong>Rose Web Design</strong>
    </div>
  </v-footer>
</template>

<script>
  export default {
    data: () => ({
      icons: [
        'mdi-facebook',
        'mdi-twitter',
        'mdi-linkedin',
        'mdi-instagram',
      ],
    }),
    methods: {
      navigateToSocialMedia(icon) {
        let socialMediaURL = '';

        switch(icon) {
          case 'mdi-facebook':
            socialMediaURL = 'https://www.google.com';
            break;
          case 'mdi-twitter':
            socialMediaURL = 'https://www.google.com';
            break;
          case 'mdi-linkedin':
            socialMediaURL = 'https://www.google.com';
            break;
          case 'mdi-instagram':
            socialMediaURL = 'https://www.google.com';
            break;
          default:
            socialMediaURL = '';
        }

        if (socialMediaURL) {
          window.open(socialMediaURL, '_blank');
        }
      }
    }
  }
</script>