<template>
  <v-app>
    <HeaderVue />
    <div class="container my-12 mx-auto px-4">
      <v-row class="d-flex" cols="12">
        <v-col>
          <v-select
            v-model="searchModel.categoryId"
            label="Kategori"
            :items="categories"
            item-title="name"
            item-value="id"
            xs="4"
            sm="3"
            lg="3"
          ></v-select>
        </v-col>

        <v-col xs="4" sm="6" lg="6"
          ><v-text-field
            v-model="searchModel.searchText"
            label="Film ara"
          ></v-text-field
        ></v-col>
        <v-col xs="4" sm="3" lg="3"
          ><v-select
            :items="sortItems"
            label="Sırala"
            v-model="sortBy"
            solo
          ></v-select>
        </v-col>
      </v-row>
      <div
        v-if="!filteredMovies.length"
        class="flex flex-row align-center justify-center pt-36"
      >
        <p class="text-xl font-sans text-slate-400">
          Listelenecek film bulunamadı
        </p>
      </div>
      <v-dialog v-model="dialog">
        <v-card class="flex w-80 mx-auto">
          <v-card-text> {{ deletedFilm?.title }} silinsin mi ?</v-card-text>
          <v-card-actions>
            <v-btn color="red" @click="deleteMovie()" class="ml-auto mr-0"
              >Sil</v-btn
            >
            <v-btn @click="dialog = false"> İptal </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>

      <v-row>
        <v-col
          cols="6"
          sm="4"
          md="3"
          lg="2"
          class="position-relative"
          v-for="(item, index) in filteredMovies.slice(0, showedFilmCount)"
          :key="index"
          @click="handleDetail(item)"
        >
          <v-hover v-slot="{ isHovering, props }">
            <v-card
              :elevation="isHovering ? 12 : 2"
              :class="{ 'on-hover': isHovering }"
              class="flex flex-wrap cursor-pointer"
              v-bind="props"
            >
              <div class="d-flex" v-if="this.user?.isAdmin">
                <button
                  v-if="isHovering"
                  @click.stop.prevent="openDeleteModal($event, item)"
                  color="primary"
                  class="absolute z-10 w-7 bg-red-500 rounded-full font-sans absolute right-1 top-1 p-1 text-white text-sm md:text-xs"
                >
                  Sil
                </button>
                <!-- <button
                    v-if="isHovering"
                    @click="update(index)"
                    class="absolute z-10 bg-yellow-500 rounded-full font-sans absolute p-1 right-10 top-1 text-white text-sm md:text-xs"
                  >
                    Güncelle
                  </button> -->
              </div>

              <div
                v-if="isHovering"
                class="absolute text-center d-flex flex-column left-0 right-0 top-32 text-white font-bold xl:top-32"
              >
                <h1
                  class="text-lg md:text-xl lg:text-xl xl:text-2xl"
                  style="color: white"
                >
                  {{ item.title }}
                </h1>
                <h1
                  class="text-yellow pt-12 text-5xl sm:text-5xl md:text-6xl lg:text-6xl xl:text-6xl"
                >
                  {{ item.point }}
                </h1>
                <!-- <h1
                    class="text-yellow pt-12 text-5xl sm:text-5xl md:text-6xl lg:text-6xl xl:text-6xl"
                  >
                    {{ item.category }}
                  </h1> -->
              </div>
              <img
                alt="Placeholder"
                class="block h-80 w-full object-cover"
                :src="'http://localhost:7224/Upload/MovieImages/' + item.poster"
              />
            </v-card>
          </v-hover>
        </v-col>
      </v-row>

      <div
        v-if="
          filteredMovies.length > 10 && showedFilmCount < filteredMovies.length
        "
        class="d-flex flex-row justify-space-around p-10"
      >
        <v-btn elevation="2" color="yellow lighten-1" @click="isShowMore()"
          >Devamını göster</v-btn
        >
      </div>
    </div>
    <FooterVue class="position-relative" />
  </v-app>
</template>

<script>
import { toRaw } from "vue";
import HeaderVue from "./page-header.vue";
import FooterVue from "./page-footer.vue";
import axios from "axios";

export default {
  name: "ListingPage",
  props: {
    msg: String,
  },
  components: {
    HeaderVue,
    FooterVue,
  },
  data() {
    return {
      movies: [],
      user: null,
      filteredMovies: [],
      searchModel: {
        categoryId: null,
        searchText: "",
      },
      sortItems: ["Artan puan", "Azalan puan"],
      sortBy: "",
      showedFilmCount: 10,
      dialog: false,
      deletedFilmIndex: 0,
      categories: [],
    };
  },
  methods: {
    handleDetail(item) {
      this.$router.push({ path: `/film-detail/${item.id}` });
    },
    deleteMovie() {
      console.log("deleteCard deleted", this.deletedFilm);
      axios
        .delete(`http://localhost:7224/api/Movies/${this.deletedFilm.id}`)
        .then((res) => {
          console.log("movie deleted res", res);
          this.getMovies();
        })
        .finally(() => (this.isLoading = false));
      this.dialog = false;
    },
    sortByAsc(a, b) {
      return parseInt(a.point) - parseInt(b.point);
    },
    sortByDesc(a, b) {
      return parseInt(b.point) - parseInt(a.point);
    },
    update(index) {
      this.$router.push({ path: `/update/${index}` });
    },
    getFilmsByName() {
      this.filteredMovies = this.movies.filter((film) => {
        return film.title.toLowerCase().includes(this.filmName.toLowerCase());
      });
    },
    isShowMore() {
      this.showedFilmCount += 10;
    },
    openDeleteModal(event, item) {
      console.log("openDeleteModal deleted", item);

      this.dialog = true;
      this.deletedFilm = item;
    },
    getMovies() {
      axios
        .get(`http://localhost:7224/api/Movies/GetAll`)
        .then((res) => {
          console.log("movies res", res);
          this.movies = res.data;
          this.filteredMovies = res.data;
        })
        .finally(() => (this.isLoading = false));
    },

    getCategories() {
      axios
        .get(`http://localhost:7224/api/Category/GetAll`)
        .then((res) => {
          this.categories = res.data;
          this.categories.unshift({ name: "Kategori seçiniz", id: "" });
          console.log("get categories", toRaw(this.categories));
        })
        .finally(() => (this.isLoading = false));
    },
  },
  mounted() {
    this.user = JSON.parse(localStorage.getItem("user"));
    console.log("user", this.user);
    if (this.user == null) {
      this.$router.push({ path: "/login" });
    }
    this.getMovies();
    this.getCategories();
  },
  watch: {
    sortBy(newValue) {
      if (newValue == "Artan puan") {
        this.movies.sort(this.sortByAsc);
      } else {
        this.movies.sort(this.sortByDesc);
      }
    },
    selectedCategory() {
      const result = [];
      this.movies.forEach((movie) => {
        console.log("for movies", movie);
        if (movie.categoryId == this.selectedCategory) {
          result.push(movie);
        }
      });
      console.log("selectedCategory value", this.selectedCategory);
      console.log("result", result);
      this.filteredMovies = result;
    },
    searchModel: {
      handler() {
        this.filteredMovies = this.movies.filter((movie) => {
          const matchesCategory = this.searchModel.categoryId
            ? movie.categoryId === this.searchModel.categoryId
            : true;

          const matchesSearchText = movie.title
            .toLowerCase()
            .includes(this.searchModel.searchText.toLowerCase());

          return matchesCategory && matchesSearchText;
        });
      },
      deep: true,
    },
  },
};
</script>

<style scoped>
.v-card {
  transition: opacity 0.4s ease-in-out;
}

.v-card.on-hover {
  opacity: 0.7;
}

.show-btns {
  color: rgb(44, 44, 44) !important;
}
</style>
