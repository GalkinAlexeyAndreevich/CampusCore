import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { AppBase } from '../shared/components/appBase';
import { Layout } from '../shared/components/layout';
import { InfrastructureRouter } from './infrastructure/router/infrastructureRouter';
import { ProductsRouter } from './products/router/productsRouter';

export function App() {
	return (
		<AppBase>
			<BrowserRouter>
				<Routes>
					<Route element={<Layout />}>
						{InfrastructureRouter()}
						{ProductsRouter()}
					</Route>
				</Routes>
			</BrowserRouter>
		</AppBase>
	);
}
