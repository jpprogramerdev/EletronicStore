﻿namespace EletronicStore.Models {
    public class Telefone : EntidadeDominio {
        public string Numero { get; set; }
        public string DDD { get; set; }
        public TipoTelefone TipoTelefone {  get; set; }
    }
}
