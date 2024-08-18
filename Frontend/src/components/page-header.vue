<template>
  <div class="bg-slate-200 p-3 d-flex justify-between">
    <div class="d-flex align-center">
      <router-link to="/listing" href="">
        <img src="../assets/imgs/IMDB.png" alt="" class="w-20"
      /></router-link>
    </div>
    <div>
      <router-link to="/create" href="" class="mr-2"
        ><v-btn elevation="2" color="yellow lighten-1"
          >Film Ekle</v-btn
        ></router-link
      >
      <router-link to="/" href=""
        ><v-btn elevation="2" color="red lighten-1" @click="logOut" class="mr-2"
          >Çıkış yap</v-btn
        >
      </router-link>
      <router-link to="/profile" href=""
        ><v-btn
          density="comfortable"
          icon="mdi mdi-account"
          class="bg-slate-200"
          elevation="6"
        ></v-btn>
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    msg: String,
  },

  methods: {
    logOut() {
      localStorage.removeItem("user");
      this.$router.push({ path: "/login" });
    },
  },
};
</script>

<style scoped></style>
