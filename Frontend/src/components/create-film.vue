<template>
  <HeaderVue />
  <div>
    <div class="container my-12 mx-auto px-4 md:px-4">
      <div class="d-flex flex-col items-center pb-4">
        <v-form v-model="valid" class="sm:w-1/4 md:w-1/4" ref="form">
          <v-col class="d-flex flex-col items-center mb-10">
            <p class="text-4xl font-sans">Film Ekle</p>
          </v-col>

          <v-col>
            <v-text-field
              v-model="name"
              :rules="nameRules"
              label="Film Adı"
              required
            ></v-text-field>
          </v-col>

          <v-col>
            <v-file-input
              :rules="fileRules"
              label="Film Görseli"
              type="file"
              prepend-icon=""
              prepend-inner-icon="mdi mdi-attachment"
              @change="onFileSelected"
            ></v-file-input>
          </v-col>

          <v-col>
            <v-select
              v-model="category"
              label="Film Kategorisi"
              :rules="categoryRules"
              item-title="name"
              item-value="id"
              :items="categories"
            ></v-select>
          </v-col>

          <v-col>
            <v-text-field
              v-model="point"
              :rules="pointRules"
              label="Film Puanı"
              required
              type="number"
              min="0"
              max="9"
              @keypress="isNumber($event)"
              @blur="limitInput"
            ></v-text-field>
          </v-col>

          <v-col>
            <v-textarea
              v-model="desc"
              label="Film Açıklaması"
              :rules="descRules"
              row-height="30"
              rows="4"
              variant="filled"
              auto-grow
              shaped
              @keyup.enter="save"
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

        <v-snackbar v-model="snackbar">
          {{ snackbarText }}
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
    valid: false,
    nameRules: [(v) => !!v || "Film adı gerekli"],
    fileRules: [
      (v) => !!v || "Film resmi gerekli",
      (v) => (v && v.length > 0) || "Film resmi gerekli",
    ],
    categoryRules: [(v) => !!v || "Film kategorisi gerekli"],
    pointRules: [
      (v) => !!v || "Film puanı 0 ile 9 arasında olmalı",
      (v) => (v >= 0 && v <= 9) || "Puan 0 ile 9 arasında olmalı",
    ],
    descRules: [(v) => !!v || "Film açıklaması gerekli"],
  }),
  watch: {
    search(val) {
      if (val.length < 3) return;
      if (this.isLoading) return;

      this.isLoading = true;
      this.films = [];
    },
    model(val) {
      this.name = val.Title;
      this.image = val.Poster;
      this.category = val.category;
    },
  },

  methods: {
    resetForm() {
      this.name = "";
      this.selectedFile = null;
      this.category = "";
      this.point = 0;
      this.desc = "";
      if (this.$refs.form) {
        this.$refs.form.resetValidation();
      }
    },
    limitInput(event) {
      const value = parseFloat(event.target.value);
      if (value > 9) {
        this.point = 9;
        event.target.value = 9;
      } else if (value < 0) {
        this.point = 0;
        event.target.value = 0;
      } else {
        this.point = value;
      }
    },
    onFileSelected(event) {
      this.selectedFile = event.target.files[0];
    },

    save() {
      if (this.$refs.form && typeof this.$refs.form.validate === "function") {
        this.$refs.form.validate();
      }

      if (!this.valid) {
        this.snackbarText = "Lütfen tüm gerekli alanları doldurun.";
        this.snackbar = true;
        setTimeout(() => {
          this.snackbar = false;
        }, 1500);
        return;
      }

      const currentUser = JSON.parse(localStorage.getItem("user"));

      const formData = new FormData();
      formData.append("poster", this.selectedFile, this.selectedFile.name);
      formData.append("point", this.point);
      formData.append("title", this.name);
      formData.append("categoryId", this.category);
      formData.append("description", this.desc);
      formData.append("userId", currentUser.userId);

      axios
        .post(`http://localhost:7224/api/Movies/Create`, formData)
        .then(() => {
          this.snackbarText = "Film başarıyla kaydedildi.";
          this.snackbar = true;
          this.resetForm();
        })
        .catch(() => {
          this.snackbarText = "Film kaydedilirken bir hata oluştu.";
          this.snackbar = true;
        })
        .finally(() => (this.isLoading = false));
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
      })
      .finally(() => (this.isLoading = false));
  },
};
</script>

<style scoped></style>
