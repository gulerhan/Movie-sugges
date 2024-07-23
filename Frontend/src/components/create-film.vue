<template>
  <HeaderVue />
  <div>
    <div class="container my-12 mx-auto px-4 md:px-12">
      <!-- <v-row class="d-flex items-center" cols="12">
        <v-col xs="6" sm="3" lg="3">
          <v-autocomplete
            v-model="model"
            clearable
            v-model:search.input="search"
            :items="films"
            :loading="isLoading"
            hide-no-data
            hide-selected
            item-title="Title"
            item-value="API"
            label=""
            placeholder="Film ara"
            return-object
          >
            <template v-slot:item="{ props, item }">
              <v-list-item
                v-bind="props"
                :prepend-avatar="item.raw.Poster"
                :title="item.raw.Title"
              ></v-list-item>
            </template>
          </v-autocomplete>
        </v-col>
      </v-row> -->
      <div class="d-flex flex-col items-center">
        <v-form v-model="valid" class="w-1/4">
          <v-col class="d-flex flex-col items-center mb-10">
            <p class="text-4xl font-sans">Film Ekle</p>
          </v-col>

          <v-col md="12">
            <v-text-field
              v-model="name"
              :rules="name"
              label="Film Adı"
              required
              hide-details="false"
            ></v-text-field>
          </v-col>

          <v-col>
            <v-file-input
              md="12"
              class="file-input"
              label="Film Görseli"
              prepend-icon=""
              type="file"
              hide-details="false"
              @change="onFileSelected"
            ></v-file-input>
          </v-col>

          <v-col md="12">
            <v-select
              v-model="category"
              label="Film Kategorisi"
              hide-details
              item-title="name"
              item-value="id"
              :items="categories"
            ></v-select>
          </v-col>

          <v-col md="12">
            <v-text-field
              v-model="point"
              :rules="point"
              label="Film Puanı"
              required
              hide-details="false"
              type="number"
              @keypress="isNumber($event)"
            ></v-text-field>
          </v-col>

          <v-col md="12">
            <v-textarea
              v-model="desc"
              label="Film Açıklaması"
              row-height="30"
              rows="4"
              variant="filled"
              auto-grow
              shaped
            ></v-textarea>
          </v-col>
        </v-form>

        <div class="">
          <v-btn @click="save" elevation="2" color="yellow lighten-1"
            >Kaydet</v-btn
          >
        </div>
        <v-snackbar v-model="snackbar">
          {{ text }}

          <template v-slot:actions>
            <v-btn color="red" variant="text" @click="snackbar = false">
              Kapat
            </v-btn>
          </template>
        </v-snackbar>
      </div>
    </div>
  </div>
</template>

<script>
import HeaderVue from "./page-header.vue";
import axios from "axios";
export default {
  name: "CreatePage",
  components: {
    HeaderVue,
  },
  data: () => ({
    snackbar: false,
    text: `Kaydedildi`,
    name: "",
    category: "",
    desc: "",
    point: 0,
    films: [],
    filmName: "",
    descriptionLimit: 60,
    isLoading: false,
    model: null,
    search: null,
    categories: [],
    selectedFile: null,
  }),
  watch: {
    search(val) {
      if (val.length < 3) return;
      if (this.isLoading) return;

      this.isLoading = true;
      this.films = [];
      const _this = this;
      axios
        .get(`http://www.omdbapi.com/?i=tt3896198&apikey=2cfbd7a0&s=${val}`)
        .then((res) => {
          res.data.Search.forEach((element) => {
            _this.films.push(element);
          });
        })
        .finally(() => (this.isLoading = false));
    },
    model(val) {
      this.name = val.Title;
      this.image = val.Poster;
      this.category = val.category;
    },
  },

  methods: {
    onFileSelected(event) {
      console.log("file event", event);
      this.selectedFile = event.target.files[0];
    },
    // getFilmsByName() {
    //   if (this.filmName.length > 2) {
    //     fetch(
    //       `http://www.omdbapi.com/?i=tt3896198&apikey=2cfbd7a0&t=${this.filmName}`
    //     ).then((response) => response.json());
    //   }
    // },
    save() {
      const formData = new FormData();
      formData.append("poster", this.selectedFile, this.selectedFile.name);
      formData.append("point", this.point);
      formData.append("title", this.name);
      formData.append("categoryId", this.category);
      formData.append("description", this.desc);

      // let film = {
      //   poster: this.selectedFile,
      //   point: this.point,
      //   title: this.name,
      //   categoryId: this.category,
      //   description: this.desc,
      // };
      axios
        .post(`http://localhost:7224/api/Movies/Create`, formData)
        .then((res) => {
          console.log("save film", res);
        })
        .finally(() => (this.isLoading = false));
      this.snackbar = true;
    },

    isNumber(evt) {
      evt = evt ? evt : window.event;
      const charCode = evt.which ? evt.which : evt.keyCode;
      if ((charCode > 0 && charCode < 11) || charCode == 45) {
        evt.preventDefault();
      } else {
        return true;
      }
    },
  },
  beforeCreate() {
    const user = localStorage.getItem("user");
    if (user == null) {
      this.$router.push({ path: "/login" });
    }
    axios
      .get(`http://localhost:7224/api/Category/GetAll`)
      .then((res) => {
        this.categories = res.data;
        console.log("get cat", this.categories);
      })
      .finally(() => (this.isLoading = false));
  },
};
</script>

<style scoped></style>
