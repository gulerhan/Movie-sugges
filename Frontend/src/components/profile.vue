<template>
  <HeaderVue />
  <div class="d-flex flex-column p-8">
    <h1 class="text-xl font-bold">
      Kullanıcı: <span class="font-normal">{{ capitalizedUserName }}</span>
    </h1>
    <h1 class="text-xl font-bold">
      E-mail: <span class="font-normal">{{ user.email }}</span>
    </h1>
  </div>
  <div class="flex flex-row justify-start p-8 pb-0">
    <p class="text-xl font-sans">Yorumlarım</p>
  </div>
  <v-table class="p-6">
    <thead>
      <tr>
        <th class="text-left">Poster</th>
        <th class="text-left">Film</th>
        <th class="text-left">Yorum</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="comment in comments" :key="comment.id">
        <td>
          <router-link
            :to="{ name: 'FilmDetail', params: { id: comment?.movieId } }"
          >
            <v-img
              :src="
                'http://localhost:7224/Upload/MovieImages/' +
                comment?.movie?.poster
              "
              class="w-24 h-24 object-cover"
            ></v-img>
          </router-link>
        </td>
        <td>{{ comment?.movie?.title }}</td>
        <td>
          {{ comment?.content }}
        </td>
      </tr>
    </tbody>
  </v-table>
  <div
    v-if="!comments.length"
    class="flex flex-row align-center justify-center pt-36"
  >
    <p class="text-xl font-sans text-slate-400">
      Henüz herhangi bir filme yorum yapmadınız
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
      comments: [],
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
        .get(
          `http://localhost:7224/api/Comment/GetUserComments/${this.user.userId}`
        )
        .then((res) => {
          this.comments = res.data;
        })
        .finally(() => (this.isLoading = false));
    },
  },
};
</script>

<style scoped></style>
