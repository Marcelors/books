import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: localStorage.getItem("token"),
    user: null
  },
  mutations: {
    SET_USER(state, user){
      state.user = user
    },
    SET_TOKEN(state, token){
      state.token = token
    }
  },
  actions: {
    setUser({commit}, user){
      commit("SET_USER", user)
    },
    setToken({commit}, token) {
      commit("SET_TOKEN", token)
      localStorage.setItem("token", token);
    }
  },
  modules: {
  }
})
