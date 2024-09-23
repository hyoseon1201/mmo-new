package com.agile.repository;

import com.agile.common.AccountRole;
import com.agile.entity.Account;
import org.junit.jupiter.api.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.security.crypto.password.PasswordEncoder;

import java.time.LocalDateTime;

@SpringBootTest
class AccountRepositoryTest {

    private static final Logger log = LoggerFactory.getLogger(AccountRepositoryTest.class);
    @Autowired
    private AccountRepository accountRepository;

    @Autowired
    private PasswordEncoder passwordEncoder;

    @Test
    public void testInsertAccount() {
        for (int i = 0; i < 10; i++) {
            Account account = Account.builder()
                    .email("account" + i + "@agile.com")
                    .password(passwordEncoder.encode("qwer1234!"))
                    .createdAt(LocalDateTime.now())
                    .updatedAt(LocalDateTime.now())
                    .build();
            account.addRole(AccountRole.USER);

            if (i >= 5) {
                account.addRole(AccountRole.MANAGER);
            }

            if (i >= 8) {
                account.addRole(AccountRole.ADMIN);
            }

            accountRepository.save(account);
        }
    }

    @Test
    public void testRead()
    {
        String email = "account9@agile.com";

        Account account = accountRepository.getWithRolesFromEmail(email);

        log.info(String.valueOf(account));
        log.info(account.getAccountRoleList().toString());
    }
}