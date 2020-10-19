<template>
  <div>
    <el-row>
      <el-col :span="20">
        <el-input
          placeholder="Pesquisar..."
          v-model="filter.search"
          class="input-with-select"
        >
          <el-button
            slot="append"
            icon="el-icon-search"
            @click="get"
          ></el-button>
        </el-input>
      </el-col>
      <el-col :span="4">
        <el-button class="button" @click="openForm"
          >Adicionar Usuário</el-button
        >
      </el-col>
    </el-row>

    <hr />

    <el-table
      :data="users"
      border
      style="width: 100%"
      empty-text="Nenhum registro encontrado"
    >
      <el-table-column prop="name" label="Nome"> </el-table-column>
      <el-table-column prop="email" label="Email"> </el-table-column>
      <el-table-column prop="profileText" label="Perfil"> </el-table-column>
      <el-table-column fixed="right" label="" width="120">
        <template slot-scope="props">
          <el-button
            @click.native.prevent="remove(props.row.id)"
            type="text"
            size="small"
          >
            Remover
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <br />
    <el-pagination
      background
      layout="prev, pager, next"
      :total="totalItems"
      @current-change="get"
      :current-page.sync="filter.currentPage"
      :page-size="filter.itemsPerPage"
    >
    </el-pagination>

    <form-page ref="formPage" v-on:addUser="get"></form-page>
  </div>
</template>

<script>
import * as service from "@/services/user.service";
import FormPage from "./Form";

export default {
  components: {
    FormPage,
  },
  data() {
    return {
      filter: {
        search: "",
        currentPage: 0,
        itemsPerPage: 10,
      },
      users: [],
      totalItems: null,
    };
  },
  mounted() {
    this.get();
  },
  methods: {
    openForm() {
      this.$refs["formPage"].handleOpen();
    },
    remove(id) {
      console.log(id);
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
            text: "usuário removido com sucesso!",
          });
          loading.close();
          this.get();
        },
        (err) => {
          this.$swal({ icon: "error", text: err });
          loading.close();
        }
      );
    },
    get() {
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });

      service.get(this.filter).then(
        (result) => {
          this.users = result.items.map((x) => {
            return Object.assign({}, x, {
              profileText: x.profileModel.text,
            });
          });
          this.totalItems = result.totalItems;
          loading.close();
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