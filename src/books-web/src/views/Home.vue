<template>
  <el-tabs type="border-card">
    <el-tab-pane label="Home">Bem Vindo {{ user.name }}</el-tab-pane>
    <el-tab-pane label="Livros"><book-page v-on:addFavorite="refresh"></book-page></el-tab-pane>
    <el-tab-pane label="Livros Favoritos"><favorite-book-page ref="favoritebook"></favorite-book-page></el-tab-pane>
    <el-tab-pane label="Usuários">Usuário</el-tab-pane>
  </el-tabs>
</template>

<script>
import { mapState } from "vuex";
import BookPage from "./book/Index"
import FavoriteBookPage from "./favorite-book/Index"

export default {
  components: {
    BookPage,
    FavoriteBookPage
  },
  computed: {
    ...mapState({
      user: (state) => state.user,
    }),
  },
  methods: {
    refresh(){
      debugger
      this.$refs["favoritebook"].getBooks()
    }
  }
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