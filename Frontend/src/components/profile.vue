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
        <th class="text-left">Point</th>
        <th class="text-left">Comment</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in desserts" :key="item.name">
        <td>{{ item.name }}</td>
        <td>{{ item.point }}</td>
        <td>{{ item.comment }}</td>
      </tr>
    </tbody>
  </v-table>
</template>

<script>
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
      desserts: [
        {
          name: "Frozen Yogurt",
          point: 7,
          comment: "asdasdasd",
        },
        {
          name: "Ice cream sandwich",
          point: 6,
          comment: "asdasdasd",
        },
        {
          name: "Eclair",
          point: 9,
          comment: "asdasdasd",
        },
        {
          name: "Cupcake",
          point: 5,
          comment: "asdasdasd",
        },
        {
          name: "Gingerbread",
          point: 4,
          comment: "asdasdasd",
        },
        {
          name: "Jelly bean",
          point: 6,
          comment: "asdasdasd",
        },
        {
          name: "Lollipop",
          point: 2,
          comment: "asdasdasd",
        },
        {
          name: "Honeycomb",
          point: 7,
          comment: "asdasdasd",
        },
        {
          name: "Donut",
          point: 5,
          comment: "asdasdasd",
        },
        {
          name: "KitKat",
          point: 1,
          comment: "asdasdasd",
        },
      ],
    };
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
  },
  // mounted() {
  //   this.fetchUser();
  // },
};
</script>

<style scoped></style>
