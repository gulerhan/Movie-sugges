<template>
  <div class="video-background">
    <video playsinline autoplay muted loop class="background-video">
      <source src="/loginBackground.mp4" type="video/mp4" />
    </video>
    <div>
      <div class="container my-12 mx-auto px-4 md:px-12">
        <div class="d-flex flex-col items-center">
          <v-form v-model="valid" class="w-3/4 sm:w-1/4" ref="form">
            <v-col class="d-flex flex-col items-center mb-2">
              <p class="text-4xl font-sans text-white">Kayıt Ol</p>
            </v-col>
            <v-col md="12">
              <v-text-field
                v-model="name"
                :rules="nameRules"
                label="Adı"
                required
                class="text-white"
              ></v-text-field>
            </v-col>
            <v-col md="12">
              <v-text-field
                v-model="surname"
                :rules="surnameRules"
                label="Soyadı"
                required
                class="pt-3 text-white"
                @keyup.enter="login"
              ></v-text-field>
            </v-col>
            <v-col md="12">
              <v-text-field
                v-model="email"
                :rules="emailRules"
                label="Email"
                required
                class="pt-3 text-white"
                @keyup.enter="login"
              ></v-text-field>
            </v-col>
            <v-col md="12" class="position-relative">
              <v-text-field
                v-model="password"
                :rules="passwordRules"
                label="Şifre"
                required
                class="pt-3 text-white"
                :type="showPassword ? 'text' : 'password'"
                @keyup.enter="register"
              ></v-text-field>
              <v-icon
                v-if="!showPassword"
                class="position-absolute top-7 right-0 mt-3 mr-6"
                @click="showPassword = !showPassword"
                color="grey lighten-1"
              >
                mdi-eye
              </v-icon>
              <v-icon
                v-else
                class="position-absolute top-7 right-0 mt-3 mr-6"
                @click="showPassword = !showPassword"
                color="grey lighten-1"
              >
                mdi-eye-off
              </v-icon>
            </v-col>
          </v-form>
          <div class="d-flex flex-col align-items-center w-3/4 sm:w-1/4">
            <v-btn
              elevation="2"
              color="light-blue-accent-3 mt-4"
              @click="register"
              >Kayıt Ol</v-btn
            >
            <v-btn elevation="2" color="yellow lighten-1 mt-4" @click="login"
              >Geri dön</v-btn
            >
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "RegisterPage",
  data() {
    return {
      name: "",
      surname: "",
      email: "",
      password: "",
      snackbarInfo: "",
      isShowSnackbar: false,
      snackbarColor: "red",
      showPassword: false,
      valid: false,
      nameRules: [(v) => !!v || "Adı gerekli"],
      surnameRules: [(v) => !!v || "Soyadı gerekli"],
      emailRules: [
        (v) =>
          /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) ||
          "E-mail geçerli olmalı",
      ],
      passwordRules: [(v) => !!v || "Şifre gerekli"],
    };
  },
  methods: {
    resetForm() {
      this.name = "";
      this.surname = null;
      this.email = "";
      this.password = 0;
      if (this.$refs.form) {
        this.$refs.form.resetValidation();
      }
    },
    register() {
      if (this.$refs.form && typeof this.$refs.form.validate === "function") {
        this.$refs.form.validate();
      } else {
        console.error("Form referansı veya validate fonksiyonu bulunamadı.");
      }

      // if (!this.valid) {
      //   this.snackbarText = "Lütfen tüm gerekli alanları doldurun.";
      //   this.snackbar = true;
      //   return;
      // }
      if (
        this.name.length == 0 ||
        this.surname.length == 0 ||
        this.email.length == 0 ||
        this.password.length == 0
      ) {
        this.isShowSnackbar.style;
        this.isShowSnackbar = true;
        return;
      }

      const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          firstName: this.name,
          lastname: this.surname,
          email: this.email,
          password: this.password,
        }),
      };

      fetch(
        `http://localhost:7224/api/Users/Registration`,
        requestOptions
      ).then((res) => {
        console.log("register res", res);
        this.snackbarInfo = "Kayıt başarılı";
        this.isShowSnackbar = true;
        this.snackbarColor = "green";
        setTimeout(() => {
          this.$router.push({ path: "/login" });
        }, 2000);
      });

      // localStorage.setItem(
      //   "user",
      //   JSON.stringify({
      //     name: this.name,
      //     surname: this.surname,
      //     email: this.email,
      //     password: this.password,
      //   })
      // );
    },
    login() {
      this.$router.push({ path: "/login" });
    },
  },
};
</script>

<style scoped>
.video-background {
  position: relative;
  width: 100%;
  height: 100vh;
  overflow: hidden;
}

#video-bg {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
  z-index: -1;
  animation: loop-video 30s infinite alternate;
}

.background-video {
  position: absolute;
  top: 50%;
  left: 50%;
  width: 100%;
  height: 100%;
  object-fit: cover;
  transform: translate(-50%, -50%);
  z-index: -1;
}

.content {
  position: relative;
  z-index: 1;
}

@keyframes loop-video {
  from {
    transform: scale(1.1);
  }

  to {
    transform: scale(1);
  }
}
</style>
