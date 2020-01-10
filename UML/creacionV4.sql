/*==============================================================*/
/* DBMS name:      ORACLE Version 12c                           */
/* Created on:     04/01/2020 22:34:05                          */
/*==============================================================*/


alter table ESTADO_RESULTADO
   drop constraint FK_ESTADO_R_RELATIONS_LIBRO_CO;

alter table FACTURA
   drop constraint FK_FACTURA_RELATIONS_TRANSACC;

alter table LIBRO_CONTABLE
   drop constraint FK_LIBRO_CO_RELATIONS_ESTADO_R;

alter table LIBRO_CONTABLE
   drop constraint FK_LIBRO_CO_RELATIONS_USUARIO;

alter table PRESUPUESTO
   drop constraint FK_PRESUPUE_RELATIONS_USUARIO;

alter table REG_DET_LIBROS
   drop constraint FK_REG_DET__RELATIONS_TRANSACC;

alter table REG_DET_LIBROS
   drop constraint FK_REG_DET__RELATIONS_LIBRO_CO;

alter table REG_DET_PRESUPUESTOS
   drop constraint FK_REG_DET__RELATIONS_PRESUPUE;

alter table TRANSACCION_PRE
   drop constraint FK_TRANSACC_PRE_RUBRO;

alter table TRANSACCION_PRE
   drop constraint FK_TRANSACC_RELATIONS_REG_DET_;

alter table TRANSACCION_REAL
   drop constraint FK_TRANSACC_REAL_RUBRO;

alter table TRANSACCION_REAL
   drop constraint FK_TRANSACC_RELATIONS_FACTURA;

alter table USUARIO
   drop constraint FK_USUARIO_RELATIONS_PERMISO;

alter table USUARIO
   drop constraint FK_USUARIO_RELATIONS_ROL;

drop index RELATIONSHIP_14_FK;

drop table ESTADO_RESULTADO cascade constraints;

drop index RELATIONSHIP_12_FK;

drop table FACTURA cascade constraints;

drop index RELATIONSHIP_13_FK;

drop index RELATIONSHIP_7_FK;

drop table LIBRO_CONTABLE cascade constraints;

drop table PERMISO cascade constraints;

drop index RELATIONSHIP_2_FK;

drop table PRESUPUESTO cascade constraints;

drop index RELATIONSHIP_6_FK;

drop index RELATIONSHIP_5_FK;

drop table REG_DET_LIBROS cascade constraints;

drop index RELATIONSHIP_4_FK;

drop table REG_DET_PRESUPUESTOS cascade constraints;

drop table ROL cascade constraints;

drop table RUBRO cascade constraints;

drop index RELATIONSHIP_16_FK;

drop index RELATIONSHIP_15_FK;

drop table TRANSACCION_PRE cascade constraints;

drop index RELATIONSHIP_11_FK;

drop index RELATIONSHIP_1_FK;

drop table TRANSACCION_REAL cascade constraints;

drop index RELATIONSHIP_17_FK;

drop index RELATIONSHIP_8_FK;

drop table USUARIO cascade constraints;

/*==============================================================*/
/* Table: ESTADO_RESULTADO                                      */
/*==============================================================*/
create table ESTADO_RESULTADO (
   ID_EST_RES           NUMBER(8)             generated as identity,
   LIB_CONTABLE_ID      NUMBER(8)             not null,
   UTILIDAD_BRUTA       NUMBER(10,3),
   UTILIDAD_ANTES_IMP   NUMBER(10,3),
   UTILIDAD_EJERCICIO   NUMBER(10,3),
   FECHA_INICIO         TIMESTAMP,
   FECHA_FIN            TIMESTAMP,
   constraint PK_ESTADO_RESULTADO primary key (ID_EST_RES)
);

/*==============================================================*/
/* Index: RELATIONSHIP_14_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_14_FK on ESTADO_RESULTADO (
   LIB_CONTABLE_ID ASC
);

/*==============================================================*/
/* Table: FACTURA                                               */
/*==============================================================*/
create table FACTURA (
   ID_FACTURA           NUMBER(8)             generated as identity,
   TRANS_REAL_ID        NUMBER(8)             not null,
   NUMERO               VARCHAR2(50),
   FOTO                 BLOB,
   constraint PK_FACTURA primary key (ID_FACTURA)
);

/*==============================================================*/
/* Index: RELATIONSHIP_12_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_12_FK on FACTURA (
   TRANS_REAL_ID ASC
);

/*==============================================================*/
/* Table: LIBRO_CONTABLE                                        */
/*==============================================================*/
create table LIBRO_CONTABLE (
   ID_LIB_CONTABLE      NUMBER(8)             generated as identity,
   USU_ID               NUMBER(8)             not null,
   EST_RES_ID           NUMBER(8),
   FECHA_INICIO         TIMESTAMP,
   FECHA_FIN            TIMESTAMP,
   TOTAL_INGRESOS       NUMBER(10,3)         default 0  not null,
   TOTAL_GASTOS         NUMBER(10,3)         default 0  not null,
   constraint PK_LIBRO_CONTABLE primary key (ID_LIB_CONTABLE)
);

/*==============================================================*/
/* Index: RELATIONSHIP_7_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_7_FK on LIBRO_CONTABLE (
   USU_ID ASC
);

/*==============================================================*/
/* Index: RELATIONSHIP_13_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_13_FK on LIBRO_CONTABLE (
   EST_RES_ID ASC
);

/*==============================================================*/
/* Table: PERMISO                                               */
/*==============================================================*/
create table PERMISO (
   ID_PERMISO           NUMBER(8)            generated as identity,
   ESCRITURA            INTEGER              default 1,
   LECTURA              INTEGER              default 1,
   constraint PK_PERMISO primary key (ID_PERMISO)
);

/*==============================================================*/
/* Table: PRESUPUESTO                                           */
/*==============================================================*/
create table PRESUPUESTO (
   ID_PRESUPUESTO       NUMBER(8)             generated as identity,
   USU_ID               NUMBER(8)             not null,
   FECHA_INICIO         TIMESTAMP,
   FECHA_FIN            TIMESTAMP,
   VALOR_PRE            NUMBER(10,3)         default 0  not null,
   TOTAL_GASTOS         NUMBER(10,3)         default 0,
   constraint PK_PRESUPUESTO primary key (ID_PRESUPUESTO)
);

/*==============================================================*/
/* Index: RELATIONSHIP_2_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_2_FK on PRESUPUESTO (
   USU_ID ASC
);

/*==============================================================*/
/* Table: REG_DET_LIBROS                                        */
/*==============================================================*/
create table REG_DET_LIBROS (
   ID_REG_DET_LIB       NUMBER(10)            generated as identity,
   TRA_ID               NUMBER(8)             not null,
   LIB_ID               NUMBER(8)             not null,
   constraint PK_REG_DET_LIBROS primary key (ID_REG_DET_LIB)
);

/*==============================================================*/
/* Index: RELATIONSHIP_5_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_5_FK on REG_DET_LIBROS (
   TRA_ID ASC
);

/*==============================================================*/
/* Index: RELATIONSHIP_6_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_6_FK on REG_DET_LIBROS (
   LIB_ID ASC
);

/*==============================================================*/
/* Table: REG_DET_PRESUPUESTOS                                  */
/*==============================================================*/
create table REG_DET_PRESUPUESTOS (
   ID_REG_DET_PRE       NUMBER(18)            generated as identity,
   PRE_ID               NUMBER(8)             not null,
   constraint PK_REG_DET_PRESUPUESTOS primary key (ID_REG_DET_PRE)
);

/*==============================================================*/
/* Index: RELATIONSHIP_4_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_4_FK on REG_DET_PRESUPUESTOS (
   PRE_ID ASC
);

/*==============================================================*/
/* Table: ROL                                                   */
/*==============================================================*/
create table ROL (
   ID_ROL               NUMBER(2)             generated as identity,
   NOMBRE               VARCHAR2(32)          not null,
   constraint PK_ROL primary key (ID_ROL)
);

/*==============================================================*/
/* Table: RUBRO                                                 */
/*==============================================================*/
create table RUBRO (
   ID_RUBRO             NUMBER(8)             generated as identity,
   NOMBRE               VARCHAR2(200)         not null,
   constraint PK_RUBRO primary key (ID_RUBRO)
);

/*==============================================================*/
/* Table: TRANSACCION_PRE                                       */
/*==============================================================*/
create table TRANSACCION_PRE (
   ID_TRANS_PRE         NUMBER(8)             generated as identity,
   RUB_ID               NUMBER(8)             not null,
   REG_DET_PRE_ID       NUMBER(18)            not null,
   TIPO                 INTEGER               not null,
   FECHA                TIMESTAMP,
   SUBTOTAL             NUMBER(10,3),
   IMPUESTO             NUMBER(10,3),
   TOTAL                NUMBER(10,3),
   constraint PK_TRANSACCION_PRE primary key (ID_TRANS_PRE)
);

/*==============================================================*/
/* Index: RELATIONSHIP_15_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_15_FK on TRANSACCION_PRE (
   RUB_ID ASC
);

/*==============================================================*/
/* Index: RELATIONSHIP_16_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_16_FK on TRANSACCION_PRE (
   REG_DET_PRE_ID ASC
);

/*==============================================================*/
/* Table: TRANSACCION_REAL                                      */
/*==============================================================*/
create table TRANSACCION_REAL (
   ID_TRANS_REAL        NUMBER(8)             generated as identity,
   RUB_ID               NUMBER(8)             not null,
   FACTURA_ID           NUMBER(8),
   TIPO                 INTEGER               not null,
   FECHA                TIMESTAMP,
   SUBTOTAL             NUMBER(10,3),
   IMPUESTO             NUMBER(10,3),
   TOTAL                NUMBER(10,3),
   constraint PK_TRANSACCION_REAL primary key (ID_TRANS_REAL)
);

/*==============================================================*/
/* Index: RELATIONSHIP_1_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_1_FK on TRANSACCION_REAL (
   RUB_ID ASC
);

/*==============================================================*/
/* Index: RELATIONSHIP_11_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_11_FK on TRANSACCION_REAL (
   FACTURA_ID ASC
);

/*==============================================================*/
/* Table: USUARIO                                               */
/*==============================================================*/
create table USUARIO (
   ID_USUARIO           NUMBER(8)             generated as identity,
   ROL_ID               NUMBER(2)             not null,
   PER_ID               NUMBER(8)             not null,
   NOMBRE_USUARIO       VARCHAR2(60)          not null,
   PASSWORD             VARCHAR2(200)         not null,
   NOMBRE               VARCHAR2(200)         not null,
   APELLIDO             VARCHAR2(200)         not null,
   CEDULA               VARCHAR2(10)          not null,
   RUC                  VARCHAR2(15)          not null,
   DIRECCION            VARCHAR2(200),
   TELEFONO             VARCHAR2(10),
   EMAIL                VARCHAR2(150),
   constraint PK_USUARIO primary key (ID_USUARIO)
);

/*==============================================================*/
/* Index: RELATIONSHIP_8_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_8_FK on USUARIO (
   ROL_ID ASC
);

/*==============================================================*/
/* Index: RELATIONSHIP_17_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_17_FK on USUARIO (
   PER_ID ASC
);

alter table ESTADO_RESULTADO
   add constraint FK_ESTADO_R_RELATIONS_LIBRO_CO foreign key (LIB_CONTABLE_ID)
      references LIBRO_CONTABLE (ID_LIB_CONTABLE);

alter table FACTURA
   add constraint FK_FACTURA_RELATIONS_TRANSACC foreign key (TRANS_REAL_ID)
      references TRANSACCION_REAL (ID_TRANS_REAL);

alter table LIBRO_CONTABLE
   add constraint FK_LIBRO_CO_RELATIONS_ESTADO_R foreign key (EST_RES_ID)
      references ESTADO_RESULTADO (ID_EST_RES);

alter table LIBRO_CONTABLE
   add constraint FK_LIBRO_CO_RELATIONS_USUARIO foreign key (USU_ID)
      references USUARIO (ID_USUARIO);

alter table PRESUPUESTO
   add constraint FK_PRESUPUE_RELATIONS_USUARIO foreign key (USU_ID)
      references USUARIO (ID_USUARIO);

alter table REG_DET_LIBROS
   add constraint FK_REG_DET__RELATIONS_TRANSACC foreign key (TRA_ID)
      references TRANSACCION_REAL (ID_TRANS_REAL);

alter table REG_DET_LIBROS
   add constraint FK_REG_DET__RELATIONS_LIBRO_CO foreign key (LIB_ID)
      references LIBRO_CONTABLE (ID_LIB_CONTABLE);

alter table REG_DET_PRESUPUESTOS
   add constraint FK_REG_DET__RELATIONS_PRESUPUE foreign key (PRE_ID)
      references PRESUPUESTO (ID_PRESUPUESTO);

alter table TRANSACCION_PRE
   add constraint FK_TRANSACC_PRE_RUBRO foreign key (RUB_ID)
      references RUBRO (ID_RUBRO);

alter table TRANSACCION_PRE
   add constraint FK_TRANSACC_RELATIONS_REG_DET_ foreign key (REG_DET_PRE_ID)
      references REG_DET_PRESUPUESTOS (ID_REG_DET_PRE);

alter table TRANSACCION_REAL
   add constraint FK_TRANSACC_REAL_RUBRO foreign key (RUB_ID)
      references RUBRO (ID_RUBRO);

alter table TRANSACCION_REAL
   add constraint FK_TRANSACC_RELATIONS_FACTURA foreign key (FACTURA_ID)
      references FACTURA (ID_FACTURA);

alter table USUARIO
   add constraint FK_USUARIO_RELATIONS_PERMISO foreign key (PER_ID)
      references PERMISO (ID_PERMISO);

alter table USUARIO
   add constraint FK_USUARIO_RELATIONS_ROL foreign key (ROL_ID)
      references ROL (ID_ROL);


/*==============================================================*/
/* Claves Unicas                                    */
/*==============================================================*/

create unique index usuario_NOMBRE_USUARIO_uindex
	on usuario (NOMBRE_USUARIO)
/