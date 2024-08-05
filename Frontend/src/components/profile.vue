<template>
  <HeaderVue />
  <div class="d-flex flex-column p-8">
    <h1 class="text-3xl font-bold">{{ user.name }}</h1>
    <h1 class="text-xl">{{ user.email }}</h1>
  </div>
  <v-table class="p-6">
    <thead>
      <tr>
        <th class="text-left">Film Name</th>
        <th class="text-left">Comments</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in desserts" :key="item.name">
        <td>{{ item.name }}</td>
        <td>{{ item.comment }}</td>
      </tr>
    </tbody>
  </v-table>
</template>

<script>
// import axios from "axios";
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
      },
      movie: null,
    };
  },
  created() {
    this.fetchUser();
  },
  mounted() {
    // this.movieDetail();
  },
  methods: {
    fetchUser() {
      const user = JSON.parse(localStorage.getItem("user"));
      if (user) {
        this.user = user;
      }
    },

    // movieDetail() {
    //   axios
    //     .get(`http://localhost:7224/api/Movies/GetDetail/${this.movieId}`)
    //     .then((res) => {
    //       console.log("Profile Detail", res);
    //       this.movie = res.data;
    //     })
    //     .finally(() => (this.isLoading = false));
    // },
  },

  // mounted() {
  //   this.fetchUser();
  // },
};
</script>

<style scoped></style>
