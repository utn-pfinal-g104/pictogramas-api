
select * from pictogramas

select * from  pictogramas p 
join Keywords k on (p.id = k.IdPictograma)
where k.Keyword like '%sexólog%' or k.Keyword like '%vulva%'

select p.*, c.*, k.* from categorias c
join PictogramasPorCategorias pc on (c.id = pc.IdCategoria)
join pictogramas p on (p.id = pc.IdPictograma)
join Keywords k on (p.id = k.IdPictograma)
where c.nombre = 'tamaño'

------------ Categorias ------------
select * from categorias 
select * from categorias  where nivel = 0
select * from PictogramasPorCategorias

select c.id, c.nombre, k.IdPictograma,k.Keyword from categorias c
join PictogramasPorCategorias pc on (c.id = pc.IdCategoria)
join pictogramas p on (p.id = pc.IdPictograma)
join Keywords k on (p.id = k.IdPictograma)
where nombre like '%categori%'

select * from categorias where id in ( 271, 401, 486) 
select * from categorias where id= 319 or categoriapadre = 585
select * from PictogramasPorCategorias where idcategoria = 323
select * from categorias c1 left join categorias c2 on (c1.categoriaPadre = c2.id) where c1.nombre like ('%nuclear%') or c1.nombre like ('%central%')
select c5.id from categorias c1 
left join categorias c2 on (c1.CategoriaPadre = c2.id) 
left join categorias c3 on (c2.CategoriaPadre = c3.id) 
left join categorias c4 on (c3.CategoriaPadre = c4.id) 
left join categorias c5 on (c4.CategoriaPadre = c5.id) where c1.id in (100))

select * from categorias c1 
left join categorias c2 on (c1.id = c2.categoriaPadre) 
left join categorias c3 on (c2.id = c3.categoriaPadre) 
where c1.nombre like ('%animal%')



select CategoriaPadre from categorias where id in (271)

select * from categoriasporusuarios cu join categorias c on (c.id = cu.categoriaid)

--------------- Keywords --------------
select * from keywords where tipo = 1
select * from keywords where keyword = 'sicologia'
select * from keywords where keyword like '%sico%'
select * from keywords where idpictograma in (359,2207,2653,2656,8532)

----------------------- 

select * from pizarras
select * from celdaPizarra

select * from usuarios

select * from estadisticas

select * from favoritosporusuarios

