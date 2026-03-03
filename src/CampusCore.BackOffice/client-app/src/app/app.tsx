import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { AppBase } from '../shared/components/appBase';
import { Layout } from '../shared/components/layout';
import { InfrastructureRouter } from './infrastructure/router/infrastructureRouter';
import { ProductsRouter } from './products/router/productsRouter';
import { StudentRouter } from './student/router/studentRouter';
import { StudentGroupRouter } from './studentGroup/router/studentGroupRouter';

export function App() {
	return (
		<AppBase>
			<BrowserRouter>
				<Routes>
					<Route element={<Layout />}>
						{InfrastructureRouter()}
						{StudentRouter()}
						{StudentGroupRouter()}
						{ProductsRouter()}
					</Route>
				</Routes>
			</BrowserRouter>
		</AppBase>
	);
}
