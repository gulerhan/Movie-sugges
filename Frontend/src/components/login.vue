<template>
  <div class="video-background">
    <video playsinline autoplay muted loop class="background-video">
      <source src="/loginBackground.mp4" type="video/mp4" />
    </video>
    <div>
      <div class="container my-12 mx-auto px-4 md:px-12">
        <div class="d-flex flex-col items-center pt-12">
          <v-form v-model="valid" class="w-3/4 sm:w-1/4">
            <v-col class="d-flex flex-col items-center mb-10">
              <p class="text-4xl font-sans text-white">Giriş Yap</p>
            </v-col>

            <v-col class="position-relative">
              <v-text-field
                v-model="email"
                label="Email"
                required
                hide-details="false"
                class="text-white"
              ></v-text-field>
            </v-col>
            <v-col class="position-relative">
              <v-text-field
                v-model="password"
                label="Şifre"
                required
                hide-details="false"
                class="pt-6 text-white"
                :type="showPassword ? 'text' : 'password'"
                @keyup.enter="login"
              ></v-text-field>
              <v-icon
                v-if="!showPassword"
                class="position-absolute top-10 right-0 mt-3 mr-6"
                @click="showPassword = !showPassword"
                color="grey lighten-1"
              >
                mdi-eye
              </v-icon>
              <v-icon
                v-else
                class="position-absolute top-10 right-0 mt-3 mr-6"
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
              color="yellow lighten-1 mt-4"
              @click="login"
              class="mt-12"
            >
              Giriş
            </v-btn>
            <v-btn
              elevation="2"
              color="light-blue-accent-4 mt-6"
              aria-colspan=""
              class="mt-6"
              @click="register"
            >
              Kayıt Ol
            </v-btn>
          </div>
        </div>
      </div>
      <v-snackbar v-model="isShowSnackbar" color="red" elevation="24">
        Kullanıcı bilgileri yanlış
        <template v-slot:actions>
          <v-btn color="white" variant="text" @click="isShowSnackbar = false">
            Kapat
          </v-btn>
        </template>
      </v-snackbar>
    </div>
  </div>
</template>

<script>
export default {
  name: "LoginPage",
  data() {
    return {
      email: "",
      password: "",
      isShowSnackbar: false,
      showPassword: false,
    };
  },
  methods: {
    login() {
      if (!this.email || !this.password) {
        this.isShowSnackbar = true;
        setTimeout(() => {
          this.isShowSnackbar = false;
        }, 1500);
        return;
      }
      const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          email: this.email,
          password: this.password,
        }),
      };
      fetch(`http://localhost:7224/api/Users/Login`, requestOptions)
        .then((response) => {
          if (!response.ok) {
            console.log("response", response);
            if (response.status == 404) {
              this.isShowSnackbar = true;
            }
            throw new Error(`HTTP error! Status: ${response.message}`);
          }
          return response.json();
        })
        .then((res) => {
          const user = {
            name: res.firstName + " " + res.lastName,
            email: res.email,
            userId: res.userId,
            token: res.token,
            isAdmin: res.roleId == 1 ? true : false,
          };
          console.log("login res", res);
          localStorage.setItem("user", JSON.stringify(user));
          this.$router.push({ path: "/listing" });
        })
        .catch((error) => {
          console.error("Hata oluştu:", error.message);
        });
    },
    register() {
      this.$router.push({ path: "/register" });
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
