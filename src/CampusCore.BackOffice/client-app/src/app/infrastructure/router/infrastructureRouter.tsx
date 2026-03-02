import React from 'react';
import { Route } from 'react-router-dom';
import { ProductsPage } from '../../products/productsPage';
import { InfrastructureLink } from './infrastructureLink';

export function InfrastructureRouter() {
	return (
		<>
			<Route path={InfrastructureLink.index} element={<ProductsPage />} />
		</>
	);
}
