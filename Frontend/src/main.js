import { createApp } from "vue";
import App from "./App.vue";
import "./index.css";
import "vuetify/styles";
import { createVuetify } from "vuetify";
import * as components from "vuetify/components";
import * as directives from "vuetify/directives";
import { createRouter, createWebHashHistory } from "vue-router";
import '@mdi/font/css/materialdesignicons.css';
import ListingPage from "./components/listing.vue";
import CreatePage from "./components/create-film.vue";
import UpdatePage from "./components/update-film.vue";
import LoginPage from "./components/login.vue";
import RegisterPage from "./components/register.vue";
import FilmDetail from "./components/film-detail.vue";
import Profile from "./components/profile.vue";



const app = createApp(App);
const vuetify = createVuetify({
  components,
  directives,
});
app.use(vuetify);
const routes = [
  { path: "/listing", component: ListingPage },
  { path: "/create", component: CreatePage },
  { path: "/update", component: UpdatePage },
  { path: "/update/:id", component: UpdatePage },
  { path: "/login", component: LoginPage },
  { path: "/register", component: RegisterPage },
  { path: "/film-detail", component: FilmDetail },
  { path: "/film-detail/:id", component: FilmDetail },
  { path: "/profile", component: Profile },
  
  { path: "/", component: LoginPage },
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});
app.use(router);
app.mount("#app");
