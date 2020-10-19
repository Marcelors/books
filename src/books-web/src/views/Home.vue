<template>
  <el-tabs type="border-card">
    <el-tab-pane label="Home"
      ><div>
        Bem Vindo {{ user.name }} <br />
        <el-button class="button" style="margin: 10px" @click="exit"
          >Sair</el-button
        >
        <br />
        <ul>
          <li>
            Quando ocorrer o Error 401 clique no botão sair e logue novamente
          </li>
        </ul>
      </div></el-tab-pane
    >
    <el-tab-pane label="Livros"
      ><book-page v-on:addFavorite="refresh"></book-page
    ></el-tab-pane>
    <el-tab-pane label="Livros Favoritos"
      ><favorite-book-page ref="favoritebook"></favorite-book-page
    ></el-tab-pane >
    <el-tab-pane label="Usuários" v-if="user.profile != 1"><user-page></user-page></el-tab-pane>
  </el-tabs>
</template>

<script>
import { mapState, mapActions } from "vuex";
import BookPage from "./book/Index";
import FavoriteBookPage from "./favorite-book/Index";
import UserPage from "./user/Index";

export default {
  components: {
    BookPage,
    FavoriteBookPage,
    UserPage,
  },
  computed: {
    ...mapState({
      user: (state) => state.user,
    }),
  },
  mounted() {
    if (this.user == null) {
      this.clean();
      this.$router.push("/");
    }
  },
  methods: {
    ...mapActions({
      clean: "clean",
    }),
    refresh() {
      this.$refs["favoritebook"].getBooks();
    },
    exit() {
      this.clean();
      this.$router.push("/");
    },
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #73787c;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>