<template>
  <div>
    <HeaderVue />
    <v-container fluid>
      <v-row>
        <v-col cols="4">
          <v-card class="m-12" max-width="420">
            <v-img
              :src="'http://localhost:7224/Upload/MovieImages/' + movie?.poster"
            ></v-img>
          </v-card>
          <v-col class="text-xl text-center font-bold">
            Kategori:
            <span class="font-normal">{{ movie?.categoryName }}</span>
          </v-col>
        </v-col>
        <v-col cols="8">
          <v-row>
            <v-col class="my-12">
              <h1 class="text-xl text-center font-bold">{{ movie?.title }}</h1>
              <h3 class="mt-6 leading-loose">
                {{ movie?.description }}
              </h3>
            </v-col>
          </v-row>
        </v-col>
      </v-row>
    </v-container>
    <v-container class="pt-8">
      <v-col
        class="d-flex align-items-center justify-center"
        v-if="!movie?.comments.length"
      >
        <p class="opacity-60">Henüz yorum eklenmedi</p>
      </v-col>
      <v-col
        class="d-flex bg-slate-100 rounded"
        v-else
        v-for="(comment, index) in movie?.comments"
        :key="index"
      >
        <p class="font-bold">{{ comment.userName }}:</p>
        <p class="pl-4">{{ comment.content }}</p>
      </v-col>
      <v-col style="padding: 0 !important" class="mt-4">
        <v-textarea
          class=""
          label="Yorumu yazınız"
          v-model="newCommentContent"
          rows="4"
          outlined
        ></v-textarea>
        <v-col class="d-flex align-items-center justify-center">
          <v-btn class="bg-yellow lighten-1" @click="addComment()"
            >Yorum yap</v-btn
          >
        </v-col>
      </v-col>
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
    newCommentContent: "",
    user: "",
    isLoading: false,
  }),
  mounted() {
    this.movieId = this.$route.params.id;
    this.fetchMovieDetail();
    this.fetchComments();
  },
  created() {
    this.fetchUser();
  },
  methods: {
    fetchUser() {
      const user = JSON.parse(localStorage.getItem("user"));
      if (user) {
        this.user = user;
      }
    },
    fetchMovieDetail() {
      axios
        .get(`http://localhost:7224/api/Movies/GetDetail/${this.movieId}`)
        .then((res) => {
          this.movie = res.data;
        })
        .finally(() => (this.isLoading = false));
    },
    fetchComments() {
      axios
        .get(
          `http://localhost:7224/api/Comment/GetMovieComments/${this.movieId}`
        )
        .then(() => {})
        .finally(() => (this.isLoading = false));
    },
    addComment() {
      const commentModel = {
        content: this.newCommentContent,
        userId: this.user.userId,
        movieId: this.movieId,
      };

      axios
        .post(`http://localhost:7224/api/Movies/Comment`, commentModel)
        .then(() => {
          this.fetchMovieDetail();
          this.newCommentContent = "";
        })
        .finally(() => (this.isLoading = false));
    },
  },
};
</script>

<style scoped></style>
