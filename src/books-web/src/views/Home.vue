<template>
  <el-tabs type="border-card">
    <el-tab-pane label="Home">Bem Vindo {{ user.name }}</el-tab-pane>
    <el-tab-pane label="Livros"><book-page></book-page></el-tab-pane>
    <el-tab-pane label="Livros Favoritos">Livros Favoritos</el-tab-pane>
    <el-tab-pane label="Usuários">Usuário</el-tab-pane>
  </el-tabs>
</template>

<script>
import { mapState } from "vuex";
import BookPage from "./book/Index"

export default {
  components: {
    BookPage
  },
  computed: {
    ...mapState({
      user: (state) => state.user,
    }),
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