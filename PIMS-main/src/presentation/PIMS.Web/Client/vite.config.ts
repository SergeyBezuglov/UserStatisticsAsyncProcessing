import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueJsx from '@vitejs/plugin-vue-jsx'
import mkcert from 'vite-plugin-mkcert'

const certKeyName='developerCert.key';
const certPemName='developerCert.pem';
const certBasePath='cert';
// https://vitejs.dev/config/
export default defineConfig({
  build: {
    outDir: 'dist',
    emptyOutDir: true
  },
  plugins: [vue(), vueJsx(),mkcert({savePath:certBasePath,keyFileName:certKeyName,certFileName:certPemName})],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    port: 5173, 
    strictPort: true,
    https: {
      cert: certBasePath+'/'+certPemName,
      key: certBasePath+'/'+certKeyName
    },    
    proxy: {
      '/api' : {
        target: 'https://localhost:7085',
        changeOrigin: true,
        secure: false,
        rewrite: (path) => path.replace(/^\/api/, '/api')
      }
    } 
  }
})
