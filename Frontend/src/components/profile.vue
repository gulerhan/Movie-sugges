<template>
  <HeaderVue />
  <div class="d-flex flex-column p-8">
    <h1 class="text-3xl font-bold">{{ capitalizedUserName }}</h1>
    <h1 class="text-xl">{{ user.email }}</h1>
  </div>
  <v-table class="p-6">
    <thead>
      <tr>
        <th class="text-left">Poster</th>
        <th class="text-left">Film Name</th>
        <th class="text-left">Comments</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="movie in movies" :key="movie.id">
        <td>
          <img :src="movie.poster" alt="" class="w-24 h-24 object-cover" />
        </td>
        <td>{{ movie.title }}</td>
        <td>
          <ul>
            <li v-for="comment in movie.comments" :key="comment.id">
              {{ comment.content }} —
              <b
                ><i>{{ comment.userName }}</i></b
              >
            </li>
          </ul>
        </td>
      </tr>
    </tbody>
  </v-table>
  <div
    v-if="!movies.length"
    class="flex flex-row align-center justify-center pt-36"
  >
    <p class="text-xl font-sans text-slate-400">
      Henüz yorum herhangi bir filme yorum yapılmadı
    </p>
  </div>
</template>

<script>
import axios from "axios";
import HeaderVue from "./page-header.vue";

export default {
  name: "UpdateFilm",
  components: {
    HeaderVue,
  },
  data() {
    return {
      user: {
        email: "",
        userId: null,
        name: "",
      },
      movies: [],
      isLoading: true,
    };
  },

  computed: {
    capitalizedUserName() {
      if (!this.user.name) return "";
      return this.user.name.charAt(0).toUpperCase() + this.user.name.slice(1);
    },
  },
  created() {
    this.fetchUser();
  },
  mounted() {
    this.fetchMyMovies();
  },
  methods: {
    fetchUser() {
      const user = JSON.parse(localStorage.getItem("user"));
      if (user) {
        this.user = user;
      }
    },

    fetchMyMovies() {
      axios
        .get(`http://localhost:7224/api/Movies/GetMyMovies/${this.user.userId}`)
        .then((res) => {
          console.log("GetMyMovies", res);
          this.movies = res.data;
        })
        .finally(() => (this.isLoading = false));
    },
  },
};
</script>

<style scoped></style>
