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
              <h1 class="text-xl text-center">{{ movie?.title }}</h1>
              <h3 class="mt-6 px-8">
                {{ movie?.description }}
              </h3>
            </v-col>
          </v-row>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import axios from "axios";
import HeaderVue from "./page-header.vue";

export default {
  name: "FilmDetail",
  components: {
    HeaderVue,
  },
  data: () => ({
    movieId: null,
    movie: null,
  }),
  mounted() {
    console.log(" id", this.$route.params.id);
    this.movieId = this.$route.params.id;
    axios
      .get(`http://localhost:7224/api/Movies/GetDetail/${this.movieId}`)
      .then((res) => {
        console.log("Movie Detail", res);
        this.movie = res.data;
      })
      .finally(() => (this.isLoading = false));
  },
};
</script>

<style scoped></style>
