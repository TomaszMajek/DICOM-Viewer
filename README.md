# DICOM Viewer - Simple medical image processing

The program loads volumes in 512x512 resolution and then presents them as a bitmap. Can freely navigate through the images. 
There are 3 different views: axial, sagitall and coronal.

Using the checkboxes in the right panel, there are 4 preview options:
* Pixel average
* First Hit
* Max
* Region Growing Segmentation

---

The Region Growing works as follows:

1. select an area in the Axial view
2. click somewhere to mark the starting point
3. click the Region Growing checkbox

---

There is also histogram under axial view.
Also you can zoom-in any area on axial view.

---

Image gallery

1. Main view
![main_view](https://lh3.googleusercontent.com/pw/ACtC-3caVSYz82An3siw1O1b-BJa8Y3EACvaD_aA6aBPZDdffqpaUgxaahy033_h1hfuuYyKZp6EyhtFBBt6ygJiHvL3HAtHHy_NZv4fTbsJHvkhPYPx55Qsfl2C3Jthrs_hmYfgHqePblrIQkCO8skn-sqP=w1200-h725-no?authuser=0)

2. Pixel average
![pixel_average](https://lh3.googleusercontent.com/3RRm8DxNJM9VwxJbpy6JAL8lwmBWosczNb3rFHaMYP7_fI-qhmCRAONrxzCW3J7XcLlfTx2Ww_sPDpkjCoNKq3Pnlvh69DUZ0_8OcI4-sWXOf-4yKx2nvhTfDsVc7dcBadUDVyZa5nucasmXD9vcdVYAwNZQ2byhsV5W_8_YbxWLtV0IyxdOofjvS7Yj6DjmXQZwF0hm9GPBp71pXMJVGxvIzML8njhAbg8vwtiaRhs0ggYFFoKE744xuBA4-H0sce89_bj-6Alg7TJhooeV9F4xkGThlLZ8E9irB1jJUVixXCxRUbfE1SCfaACozCo-clxqKeWBavXS44c-Ip2BZT-l7Yxqpnue8VYuBB3Vij85x03LdFu_tfeV8qto_Ug-faMEjqSr_VRZsH9kIa_FF2EErBqD1Br2hRmpkv_kh6TS2c_G6E9n1hZs1N_s_TQKR604X3Mfc-osiQdps8Uz14K_gib3GsIHJ4V2pVWuWja57mNlhDokd5gvbei7GVPRfJqoRL9y5XceIFZ42juO6adspb7_5kHtaopva9L_erQ_hOAyOdamThM60Of_PUofqCeNvr9IKFWsSF8MymrG5RnvitL-mc9m4Vu_CqSZ-vJs45NDXqOW9X8ZvXPZotg7AlYtmz52H3c62FxWzliZhTDgcxTxX8bn_5gtQ5xBiZ4ISgsnnR5NPx1JVjFNhQKiWWJ2iCGWephebtV7P6_viC8=w1200-h725-no?authuser=0)

3. First hit
![first_hit](https://lh3.googleusercontent.com/Fq31-t5SShJOk6P3X0PsE57bNwapX5MsAypXLnxy-Vj6RDRyjc8U8GDKrnh99V01aqMwHcx9nAn14HmMfZRHDLNexCYJ00mEoYL6PQwyIScsGEkZLj_V97LF1ArPzF4fbyHuVReQl4-UVPl25HDxtq4zMrE_mT_8WMrJ7sxSe4bR1V8brC9vzfgyBMrnLdytDdJO1F8TDwbytOXrNFiw3zCgA331MwbNhBR_4xn8-kDjB4yg_s-OE4Qb5iCM_6SqeoMDa9GA5S9eDeTp1vr_SWyYGGDfFbzhAD-0SM-rN7jsu0kipqqNKOOqTO-k0ykI66i0gTff6jzbEt3qaaF8C2120ojkOb57O59jEzRUr1oJEtbG6-K7T79jmvK9SHZqWsd09pSQpRNbszGaQKkVDQxrAHDehb7M9nEh_CR5kce-sDA2_jETmHvlYblIZpcIV3LfdGwT95vtGHNI5FSjO-D8mTCsn9OQ4W7ojNpXtUNNe9Tbv0Bz4zMPBtlCLMWqrVlV8RrRm4c-sG3oJlf5KXZYstZc5sn0d86mnlc1J7qrQO6J0zwoVXwwMUjTwHOQX5ViAkr7-LuhZZiM84fIHoimw7nXfKY_Lc7M7RUTPsFxECgglRX01Uw9RAp-n-FG_2UHBixv4-nYinhjkROlu7t-Ea60Gmh4pB2mJWfS6fVP1NMdctO9WKfofj7d10IMCgatvcUpT9SXjZt69c29k0I=w1200-h725-no?authuser=0)

4. Max
![max](https://lh3.googleusercontent.com/eM-8AwOJGgWGEi25DU-1yKLLtFz8PM4DBCDA5LIX4nRBBhF-_HbH9khQn9lpoPfb7W0Blndh3E7oRvj5KRnoimgmn1pH9i02Zb7c1-RPS5kDfM04dzgul_MMy5yUC00jDKgWIKTXS5t2tyPZ-67o3KROJRUEH1VAVWBaBOqbiPeqWeLJJxggnwLTkmVlF79Pd9KPRmFDfTc6c81TiQMkdz38g9gusqZ84plfofLNfzq88GoaYMBxoIQu_mVp9720ckMQPxQWPZT1JLVCC0p2jHMSZ_oHu_ZdUDPNRHDY4L7ZTxEG-vM_Xmwh7mZc8XjlnhguBIpQcDA5IKtlJoAVIfbusKENlEuGtKXuqw34N6-auSA6dyFqhSKy2UTKbG700aNFVETEZgNH1Hs6w4eEdY1CfC-cQJF8VjkzyoIoU9cQ4TvcQpZC2fIqatRfFXzoMLXCvflDKITQ2pvSb56ST4clDlFsWmh799tNweiVP3wyVKu1JjfCMLdqFuX2H2xcWNBZeM9GwaPQk29FlQTlQgay_IVDO7QSp1CcfewvNq-exoEt9YD1RFmwAxdsaCrFZzt7mXi-hQTk6NpImFybuz4SckgJ33qqrF6jArVEjPzaOrPqX0j_MZyfLQucS_TsmU4Ac4tng3ViTQ2x-E39r-7g4wQhaZ5o3VxdT73yoA_dMKpTt3816rPb_q0Z3gMKwNA7V417yplSYsCuIM7THX0=w1200-h725-no?authuser=0)

5. Region growing segmentation

![region_growing_segmentation](https://lh3.googleusercontent.com/84FhZ3LCLj2DbJfqkD7lOj2BjeAznzT164L1PTDbWUecoGnFJNMwuREaVm74it8RENJnD4jiginhSbP54t1H7wUPmxaUShydIrNFtTcE1R8AhGq9v4n8Uw3lewsYgrsaou3y_IujnwXTi895yFsWebMmyHbboz86TeL2Xp6bUpkSqUTR3W5Y7DY4Hheg9r90SNKLVv1kZItym0jfIIIrziBM9FPAfF7UczsQVtCb5p1GL7NGdSqFgE21lt2okE-obE2OC1p3-cCHxdARvHFK6Umth8f4Hmi2_lkQRzeopbwrSeGFKPlFpVDiscEU9jpCJmCb26rZu6XqLRgfIj6jof4u2nP9r99QLStxbUnR-abNLsG5mLpTNLzX2RZPzUFFWRV-wFctZos8CUzqPuiDoRD8rJFN6_fqF0_NV9KggzU2IQGTCcfB0Z3EvJMM7EErXA3X03rSK92s12u25ul5GNXguK9XhdLVtf17DvIaDTnx_xpbSERu2hiGpA3M7ztntXVaxYeK4fYdj-RqQoM0aBarlUmlRbIe4vFEkKeSzsm8vkEAeT1eLB1WhHFwZgprVm3S_xA6MnmNGU4Fj1GhmJ6B4NKwqvmnKFQB9ZLCY4oWXFW9KyVRcHE9SfrFLra4FxSaLLmHmNuM-6QwfpDmEDykyQSg1DNjMgbu2o3n7h_vt5a845IQJ9ELigkUi0Ab0sxWZjm0UjwIEsnPHwXV3R4=w538-h556-no?authuser=0)
![region_growing_segmentation](https://lh3.googleusercontent.com/lfxKVFoEWihzp6kCpjPP7z6p5VVCUTVwvVy5xtzZRuk6DjeCPowQrhda1hlMZoV-bWZeSGTGRw4AqNkYRA1p6B9tsHzqhdHQtW2oD1mYtA9l_e5lbffWVGv-wPwusk7BDxacnA_ggV3knHSDgDZBtbpGJywnhPxEBFrNK2RalVGAvf6GuL0QRHC0W_EvCuDH7ftz6j_C3nRg7b2FLuLk6yAWo6vdRiBUzMz83JiajFuGg0PZcdPMHzFDnHNlHVjvjNnUbIP3UoXAUY61-J0LOwyFnuj8Yv4q3zPS7t6kWJ48mUGb2ftdaTgMGkdt87nyyO76iEd9amWFELeDBDRA-da2cuHX4ALQUuR26qu_MveKx9KtGX9wrbdnP2r-v5gWT4ksxfjg21VxletAzbH9x13ZBEJoR6R11WmjlvATn9veRHq4h-iS6jyQES_IMKNA8owvH8RDeZbULAqgkP8XPELqKddYoHD9FGZ_GBCqGa4mrhoNRg8ZXVat1GcKAJRiq4IIL7zU-EclHXQmf_jknWKrCH_O5tYVFVSfK3FGlwm2zPI4IuyY7gbAZHdnzLW7Ct-XRWJbbp-IR6c67dg3M8NPKb2nuFzLyOAKeOMv48eiP0PJUpys9sRYqpJQ739sgqJ48qcFW8K1bwi2l1cfrvhiO8vxq2c2pJKQGogcoOUiuV2JQ311nF2NfFPsvK3u_5_Pdt0mVbvYQTZuukO5kjQ=w548-h550-no?authuser=0)
![region_growing_segmentation](https://lh3.googleusercontent.com/-i-O24nlgJwtBaH_tihwL1FOPe_1JRwzuNnRY8coC6zylskCuUNZcr8J1-IFZjSGgmCoM6s39H0DsgCzaDCBWfngaHMmTpWM445tmD_Ff_-Ve_eseXr8GfP352WrlEOX8d_RC-v2oU7tyS0nO_9czr2XH8G3ytkEuhy-u_5vwN4bI1rdSyIBNa0gWqrGEvkqwL5T3yb9_OFjhvK2hIFxc92CErYax0xIxjfGFtT3jJuL0PT5v0GCXnkvDb-_-iI9v7zwKQViy35ZMK_Er2YN9Fc6G9EhpMarmu35HsQ--Vq9h7ramIOUttYwdVf_-H1a5PeChvWZUBbR5B7v-qdhZAtqHMGMWVcLY2oMpicYKreMQP4j2Oqin1MIeRCIE9ehJHoLhTHw4En50nt-4V-oOp4_u_gnlHfd6K0f835Y0NVJxwY9Onv-nqfWaNpDW9FZvlHfYudPWaPS8L0u2-aEaoi_3Vrfqv8Qb6TcCZNjVpTy97qBMiaDos4N82ZgIuAqm2nPASK5f--96dM48T73DBJUZ_WR6oJrtET6-bENBjx557qVm0HuNPUjE8MMqxlCEPOc4EQiCMjxBpirZ0FBrzJNj4OiiAWmeSTnGKblNypAeb-D_-pKdmdZsqWTFwLVX3qg1nxQe2jod9Wi1MCZ_7WJWisdaER81qvPbHdLKd4Zmi17cG9il6keB3cORON7G8kenvS5uXhExWOkEw2CfBs=w540-h549-no?authuser=0)
![region_growing_segmentation](https://lh3.googleusercontent.com/Gc2hCda3BPGtvUg9epu-bG6pnLkocerKgANz0-XkHHZaNcYdMnyRnNOwP7SUdLKFjIetJWMF3poQVlEIT4p2IE9Oc5l7oWO7rA8gn3fWW_r2077WTTYPDbij6pq4KOhg1hZRvTXwfhqq2SBD6YP9oUaMdBf4DEHVDYfpuSoxS1846oWva1ypEPupankNFshqB4NEbYOHzJzRu73jMIfNFuEwZ0gsfEWSgeA7Qnt8TUNXPv16gmJgQ9A_titwXs4ogDw0FqK9TAq4d4JdT-IJ_VCcs7bBZb9q4DKX5G5nqaQA7JoMWjeb4yM2Bw58ApcVVUWOOmytmQ6rGN5RjwZ0mebPGSzW0APnaD0jWUjI4WnWDsjrs-Dabnjkra2uzVDmo2tLqyqUGfPgmXy0wDtZcm0pIesCMHsZa8MzV8ZTZoq4AKYAmKXT0h1Xji2cm9bFIsYatSs9N55hmqoJBxae4JdgVlm219GuWLkuJ4O-FVqJhwIG-2pXoBlZQnX6NDgKbCU7fMG_WsWUOBe-P-pB3ZcvlgXoPg-2K3-zEZAsQSDGBVmk92sJEoQ1ycgT6WjGzTqMnjcXSJ5IhacgOJsgOQ89viL3RDV40pmmfKWdRtI1Y3-LNs9Lhaly1t8QYzyo92W0EQik7Wr4kuvAI9cC25G8ZfVW5fQwWsX8L7P-K5ATviFFZ3P1A1Kw9qTNMiMlS_ft9Iz5rQm5xlZapiDQ6pM=w541-h557-no?authuser=0)
