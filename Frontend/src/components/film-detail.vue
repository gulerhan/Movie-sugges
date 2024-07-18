<template>
  <div>
    <HeaderVue />
    <v-container fluid>
      <v-row>
        <v-col cols="5">
          <v-card class="m-12" max-width="480">
            <v-img
              src="https://cdn.vuetifyjs.com/images/cards/cooking.png"
            ></v-img>
          </v-card>
        </v-col>
        <v-col cols="7">
          <v-row>
            <v-col class="my-12">
              <h1 class="text-xl text-center">Film İsmi</h1>
              <h3 class="mt-6 px-8">
                Lorem ipsum dolor sit amet consectetur adipisicing elit. Libero
                atque unde nobis omnis hic iste quod? Corrupti quia quos hic
                reiciendis, perspiciatis magnam fugiat ipsum. Molestias amet
                eius consequatur. Laboriosam. Exercitationem illum, quia
                consequuntur esse recusandae unde expedita temporibus, corporis
                cum laudantium earum deleniti numquam culpa. Dignissimos
                repellat hic laudantium illo eos expedita nisi? Reiciendis
                itaque exercitationem facilis architecto ipsam. Minima commodi
                fugiat nemo molestiae molestias ex saepe alias impedit libero
                officiis hic, earum repudiandae, provident nesciunt pariatur
                mollitia cumque veniam reprehenderit nobis harum voluptas
                corporis? In ratione aliquam suscipit. Nulla laudantium minima
                impedit odio reprehenderit aspernatur amet, vero ad at adipisci
                tenetur eaque laboriosam placeat magnam architecto et molestiae
                repellat modi totam iste atque! Qui nobis in cumque molestiae!
                Molestiae dolor nemo beatae eius non, doloremque impedit libero
                deserunt esse sint repellendus dolore alias vel ea ipsam sed
                odio. Ipsa perspiciatis eveniet dolorum mollitia. Est libero
                aliquid velit illum.
              </h3>
            </v-col>
          </v-row>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import HeaderVue from "./page-header.vue";

export default {
  name: "FilmDetail",
  components: {
    HeaderVue,
  },
  mounted() {
    console.log(" id", this.$route.params.id);
  },
};
</script>

<style scoped></style>
