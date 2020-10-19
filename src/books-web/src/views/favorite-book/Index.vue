<template>
  <div>
    <el-input
      placeholder="Pesquisar..."
      v-model="filter.search"
      class="input-with-select"
    >
      <el-button
        slot="append"
        icon="el-icon-search"
        @click="getBooks"
      ></el-button>
    </el-input>
    <hr />
    <el-row v-for="(book, index) in books" :key="index">
      <el-card :body-style="{ padding: '12px' }">
        <el-col :span="8">
          <img :src="book.thumbnail" v-if="book.thumbnail" class="image" />
          <br />
          <el-button
            class="button"
            style="margin: 10px"
            @click="removeFavorite(book.id)"
            >Remover dos Favoritos</el-button
          >
          <br />
        </el-col>
        <el-col :span="12">
          <div style="padding: 14px">
            <h1>{{ book.title }}</h1>
            <div class="bottom clearfix">
              <span>{{ book.description }}}</span>
            </div>
          </div>
        </el-col>
      </el-card>
      <br />
    </el-row>
    <el-pagination
      background
      layout="prev, pager, next"
      :total="totalItems"
      @current-change="getBooks"
      :current-page.sync="filter.currentPage"
      :page-size="filter.itemsPerPage"
    >
    </el-pagination>
  </div>
</template>

<script>
import * as service from "@/services/favorite-book.service";

export default {
  data() {
    return {
      filter: {
        search: "",
        currentPage: 0,
        itemsPerPage: 10,
      },
      books: [],
      totalItems: null,
    };
  },
  mounted() {
    this.getBooks();
  },
  methods: {
    getBooks() {
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });

      service.get(this.filter).then(
        (result) => {
          this.books = result.items;
          this.totalItems = result.totalItems;
          loading.close();
        },
        (err) => {
          this.$swal({ icon: "error", text: err });
          loading.close();
        }
      );
    },
    removeFavorite(id) {
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });

      service.remove(id).then(
        () => {
          this.$swal({
            icon: "success",
            text: "Livro removido dos favoritos",
          });
          loading.close();
          this.getBooks();
        },
        (err) => {
          this.$swal({ icon: "error", text: err });
          loading.close();
        }
      );
    },
  },
};
</script>