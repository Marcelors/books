<template>
  <el-dialog
    id="form-modal"
    title="Adicionar Usuário"
    :visible.sync="dialogVisible"
    width="30%"
    :before-close="handleClose"
  >
    <el-form
      :model="form"
      status-icon
      :rules="rules"
      ref="ruleForm"
      label-width="120px"
      class="demo-ruleForm"
    >
      <el-form-item label="Nome" prop="name">
        <el-input type="text" v-model="form.name" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="Email" prop="email">
        <el-input
          type="text"
          v-model="form.email"
          autocomplete="off"
        ></el-input>
      </el-form-item>
      <el-form-item label="Senha" prop="password">
        <el-input
          type="password"
          v-model="form.password"
          autocomplete="off"
        ></el-input>
      </el-form-item>
      <el-form-item label="Perfil" prop="profile">
        <el-select v-model="form.profile" placeholder="Selecione um perfil">
          <el-option
            v-for="item in options"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          >
          </el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="submitForm('ruleForm')"
          >Salvar</el-button
        >
        <el-button @click="dialogVisible = false">Cancelar</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import * as service from "@/services/user.service";

export default {
  data() {
    return {
      form: {
        name: "",
        email: "",
        password: "",
        profile: null,
      },
      rules: {
        email: [
          { required: true, message: "Email é obrigatório", trigger: "blur" },
        ],
        password: [
          { required: true, message: "Senha é obrigatória", trigger: "blur" },
        ],
        name: [
          { required: true, message: "Nome é obrigatória", trigger: "blur" },
        ],
        profile: [
          { required: true, message: "Perfil é obrigatória", trigger: "blur" },
        ],
      },
      options: [
        {
          value: 1,
          label: "Padrão",
        },
        {
          value: 2,
          label: "Administrador",
        },
      ],
      dialogVisible: false,
    };
  },
  methods: {
    handleClose() {
      this.dialogVisible = false;
    },
    handleOpen() {
      this.dialogVisible = true;
      this.form.name = "";
      this.form.email = "";
      this.form.password = "";
      this.form.profile = null;
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.add();
        }
      });
    },
    add() {
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });

      service.add(this.form).then(
        () => {
          loading.close();
          this.$swal({
            icon: "success",
            text: "Usuário adicionado com sucesso",
          });
          this.$emit("addUser");
          this.dialogVisible = false;
        },
        (err) => {
          this.$swal({
            icon: "error",
            text: err,
            target: document.getElementById("form-modal"),
          });
          loading.close();
        }
      );
    },
  },
};
</script>

<style scoped>
.swal-container {
  z-index: 100000000000 !important;
}
.swal-overlay {
  z-index: 100000000000 !important;
}
</style>